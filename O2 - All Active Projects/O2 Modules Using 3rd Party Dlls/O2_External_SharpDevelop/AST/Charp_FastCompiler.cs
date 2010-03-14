﻿using System;
using System.Reflection;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms;
using ICSharpCode.NRefactory.Ast;
using O2.DotNetWrappers.DotNet;
//O2Ref:O2SharpDevelop.dll
using ICSharpCode.NRefactory;
//O2File:C:\O2\_XRules_Local\MiscTestss\extra.cs
using O2.DotNetWrappers.ExtensionMethods;
using O2.External.SharpDevelop.ExtensionMethods;
using O2.Kernel.ExtensionMethods;

namespace O2.External.SharpDevelop.AST
{
    public class CSharp_FastCompiler
    {
        internal int forceAstBuildDelay = 100;
        public string SourceCode { get; set; }						// I think there is a small race condition with the use of this variable
        //public string OriginalCodeSnippet { get; set; }	
        public bool CreatedFromSnipptet { get; set; }
        public List<string> ReferencedAssemblies { get; set; }
        public Dictionary<string, object> InvocationParameters { get; set; }
        public CompilationUnit CompilationUnit { get; set; }
        public List<string> ExtraSourceCodeFilesToCompile { get; set; }
        public AstDetails AstDetails { get; set; }
        public string AstErrors { get; set; }
        public bool generateDebugSymbols { get; set; }
        public string CompilationErrors { get; set; }        
        public CompilerResults CompilerResults { get; set; }
        public bool ExecuteInStaThread { get; set; }
        public bool ExecuteInMtaThread { get; set; }
        
        //public O2Timer executionTime { get; set; }
        
        public MethodInvoker onAstFail { get; set; }
        public MethodInvoker onAstOK { get; set; }
        public MethodInvoker onCompileFail { get; set; }
        public MethodInvoker onCompileOK { get; set; }        
        public MethodInvoker beforeSnippetAst { get; set; }
        public MethodInvoker beforeCompile { get; set; }

        public string default_MethodName {get; set;}
        public string default_TypeName { get; set; }

		public bool DebugMode {get ; set;}
		
		Stack<string> createAstStack = new Stack<string>();
		Stack<string> compileStack = new Stack<string>();
		
		bool creatingAst;
		bool compiling;
		
        public CSharp_FastCompiler()
        {        
        	DebugMode = false;				// set to true to see details about each AstCreation and Compilation stage
        	InvocationParameters = new Dictionary<string, object>();
            ExtraSourceCodeFilesToCompile = new List<String>();
        	ReferencedAssemblies = getDefaultReferencedAssemblies();
            default_MethodName = "dynamicMethod";
            default_TypeName = "DynamicType";            
            generateDebugSymbols = false;
            //OriginalCodeSnippet = "";
            SourceCode = "";
            // defaults

        }

        public List<string> getDefaultUsingStatements()
        {
            return new List<string>().add("System")
                                     .add("System.Windows.Forms")
                                     .add("System.Drawing")
                                     .add("O2.Interfaces")
                                     .add("O2.Kernel")
                                     .add("O2.Kernel.ExtensionMethods")
                                     .add("O2.Views.ASCX.CoreControls")
                                     .add("O2.Views.ASCX.classes.MainGUI")
                                     .add("O2.DotNetWrappers.ExtensionMethods")
                                     .add("O2.External.IE.ExtensionMethods")
                                     .add("O2.XRules.Database.ExtensionMethods")
                //GraphSharp related
                                     .add("O2.Script")
                                     .add("GraphSharp.Controls")
                                     .add("O2.API.Visualization.ExtensionMethods")
                                     .add("WPF=System.Windows.Controls");
        }
		public List<string> getDefaultReferencedAssemblies()
        {
            return new List<string>().add("System.dll")
                                     .add("System.Drawing.dll")                                        
                                     .add("System.Core.dll")
                                     .add("System.Windows.Forms.dll")
                                     .add("O2_Kernel.dll")
                                     .add("O2_Interfaces.dll")
                                     .add("O2_DotNetWrappers.dll")
                                     .add("O2_Views_Ascx.dll")
                                     .add("O2_External_IE.dll")
                                     .add("O2_XRules_Database.exe")
                                     //GraphSharp related
                                     .add("O2_Api_Visualization.dll")
                                     .add("QuickGraph.dll")
                                     .add("GraphSharp.dll")
                                     .add("GraphSharp.Controls.dll")
                                     .add("PresentationCore.dll")
                                     .add("PresentationFramework.dll")
                                     .add("WindowsBase.dll")
                                     .add("WindowsFormsIntegration.dll")
                                     .add("ICSharpCode.AvalonEdit.dll");
        }

		public Dictionary<string,object> getDefaultInvocationParameters()
		{
			return new Dictionary<string, object>();
		}
              
        public void compileSnippet(string codeSnippet)
        {
            try
            {                
                createAstStack.Clear();
                if (createAstStack.Count == 0)
                    creatingAst = false;
                createAstStack.Push(codeSnippet);
                compileSnippet();
            }
            catch (Exception ex)
            {
                ex.log("in compileSnippet");
            }
        }
        public void compileSnippet()
        {
            O2Thread.mtaThread(
                () =>
                {
                    if (creatingAst == false && createAstStack.Count > 0)
                    {
                        creatingAst = true;
                        var codeSnippet = createAstStack.Pop();
                        this.sleep(forceAstBuildDelay, DebugMode);            // wait a bit to allow more entries to be cleared from the stack
                        if (createAstStack.Count > 0)
                            codeSnippet = createAstStack.Pop();

                        createAstStack.Clear();

                        InvocationParameters = getDefaultInvocationParameters();
                        this.invoke(beforeSnippetAst);
                        DebugMode.info("Compiling Source Snippet (Size: {0})", codeSnippet.size());
                        var sourceCode = createCSharpCodeWith_Class_Method_WithMethodText(codeSnippet);
                        if (sourceCode != null)
                            compileSourceCode(sourceCode, CreatedFromSnipptet);
                        creatingAst = false;
                        compileSnippet();
                    }
                });
        }

        private void compileSourceCode(string sourceCode, bool compiledFromSnipptet)
        {
            compiledFromSnipptet = compiledFromSnipptet;
            compileStack.Push(sourceCode);
            compileSourceCode();
        }

		public void compileSourceCode(string sourceCode)
        {
            compileSourceCode(sourceCode, false);
       	}
       	
       	public void compileSourceCode()
       	{
       	    O2Thread.mtaThread(
       	        () =>
       	            {
       	                if (compiling == false && compileStack.Count > 0)
       	                {
       	                    compiling = true;
                            compileExtraSourceCodeReferencesAndUpdateReferencedAssemblies();
                            this.sleep(forceAstBuildDelay, DebugMode);            // wait a bit to allow more entries to be cleared from the stack
       	                    var sourceCode = compileStack.Pop();                            
       	                    compileStack.Clear();
       	                        // remove all previous compile requests (since their source code is now out of date

       	                    //Files.setCurrentDirectoryToExecutableDirectory();                			                		
       	                    Environment.CurrentDirectory = Kernel.PublicDI.config.CurrentExecutableDirectory;
       	                    ;
       	                    this.invoke(beforeCompile);
       	                    DebugMode.info("Compiling Source Code (Size: {0})", sourceCode.size());
       	                    SourceCode = sourceCode;
       	                    var providerOptions = new Dictionary<string, string>();
       	                    providerOptions.Add("CompilerVersion", "v3.5");
       	                    var csharpCodeProvider = new Microsoft.CSharp.CSharpCodeProvider(providerOptions);
       	                    var compilerParams = new CompilerParameters
       	                                             {
       	                                                 GenerateInMemory = !generateDebugSymbols,
       	                                                 IncludeDebugInformation = generateDebugSymbols
       	                                             };

       	                    foreach (var referencedAssembly in ReferencedAssemblies)
       	                        compilerParams.ReferencedAssemblies.Add(referencedAssembly);

       	                    CompilerResults = csharpCodeProvider.CompileAssemblyFromSource(compilerParams, sourceCode);
       	                    if (CompilerResults.Errors.Count > 0 || CompilerResults.CompiledAssembly == null)
       	                    {
       	                        CompilationErrors = "";
       	                        foreach (CompilerError error in CompilerResults.Errors)
       	                        {
       	                            //CompilationErrors.Add(CompilationErrors.line(error.ToString());
       	                            CompilationErrors =
       	                                CompilationErrors.line(String.Format("{0}::{1}::{2}::{3}::{4}", error.Line,
       	                                                                     error.Column, error.ErrorNumber,
       	                                                                     error.ErrorText, error.FileName));
       	                        }
       	                        DebugMode.error("Compilation failed");
       	                        this.invoke(onCompileFail);
       	                    }
       	                    else
       	                    {
       	                        DebugMode.debug("Compilation was OK");
       	                        this.invoke(onCompileOK);
       	                    }
       	                    compiling = false;
       	                    compileSourceCode();
       	                }
       	            });
        }

        // we need to use CompileEngine (which is slower but supports multiple file compilation 
        public void compileExtraSourceCodeReferencesAndUpdateReferencedAssemblies()
        {             
            if (ExtraSourceCodeFilesToCompile.size() > 0)
            {                
                var assembly = new CompileEngine().compileSourceFiles(ExtraSourceCodeFilesToCompile);                
                if (assembly != null)
                {
                    ReferencedAssemblies.Add(assembly.Location);
                    generateDebugSymbols = true;                // if there are extra assemblies we can't generate the assembly in memory                    
                }
            }
        }
        /*public string getAstErrors(string sourceCode)
        {
            return new Ast_CSharp(sourceCode).Errors;
        }*/
        public string createCSharpCodeWith_Class_Method_WithMethodText(string code)
        {                        
            var parsedCode = TextToCodeMappings.tryToFixRawCode(code, tryToCreateCSharpCodeWith_Class_Method_WithMethodText);            
        
            if (parsedCode == null)
            {
                DebugMode.error("Ast parsing Failed");
                this.invoke(onAstFail);
            }
            return parsedCode;
        }

        public string tryToCreateCSharpCodeWith_Class_Method_WithMethodText(string code)
        {
            if (code.empty())
                return null;
			code = code.line();	// make sure there is an empty line at the end            
                      
            try
            {
                // handle special incudes in source code
                foreach(var originalLine in code.lines())
                    originalLine.starts("//include", (includeText) => 
                        {
                            if (includeText.fileExists())
                                code = code.Replace(originalLine, originalLine.line().add(includeText.contents()));
                        });  
            	var snippetParser = new SnippetParser(SupportedLanguage.CSharp);
                
                var parsedCode = snippetParser.Parse(code);
				AstErrors = snippetParser.errors();
                CompilationUnit = new CompilationUnit();

                if (parsedCode is BlockStatement || parsedCode is CompilationUnit)
                {
                    Ast_CSharp astCSharp;
                    if (parsedCode is BlockStatement)
                    {
                        // map parsedCode into a new type and method 

                        var blockStatement = (BlockStatement) parsedCode;
                        CompilationUnit.add_Type(default_TypeName)
                            .add_Method(default_MethodName, InvocationParameters, blockStatement);
                        
                        astCSharp = new Ast_CSharp(CompilationUnit);
                        astCSharp.AstDetails.mapSpecials(snippetParser.Specials);
                        // add references included in the original source code file
                        mapCodeO2References(astCSharp);
                        CreatedFromSnipptet = true;
                    }
                    else
                    {
                        CompilationUnit = (CompilationUnit)parsedCode;
                        astCSharp = new Ast_CSharp(CompilationUnit);
                        CreatedFromSnipptet = false;
                    }

                    // create sourceCode using Ast_CSharp & AstDetails		
                    if(CompilationUnit.Children.Count > 0)
                    {
                        // add the comments from the original code
                        astCSharp.extraSpecials.AddRange(snippetParser.Specials);	                 
                        // reset the astCSharp.AstDetails object        	
                        astCSharp.mapAstDetails();        	        	
                        
	                    SourceCode = astCSharp.AstDetails.CSharpCode;

                        //once we have the created SourceCode we need to create a new AST with it
                        var tempAstDetails = new Ast_CSharp(SourceCode).AstDetails;
                        //note we should try to add back the specials here (so that comments make it to the generated code
                        AstDetails = tempAstDetails;
	                    DebugMode.debug("Ast parsing was OK");
	                    this.invoke(onAstOK);
	                    return SourceCode;
                    }
                }            
            }
            catch (Exception ex)
            {                            	
				DebugMode.error("in createCSharpCodeWith_Class_Method_WithMethodText:{0}", ex.Message);                
            }      			
			return null;                
        }        

        public void mapCodeO2References(Ast_CSharp astCSharp)
        {
            generateDebugSymbols = false; // default to not generating debug symbols and creating the assembly only in memory
            ExtraSourceCodeFilesToCompile = new List<string>();
        	ReferencedAssemblies = getDefaultReferencedAssemblies();
        	var compilationUnit = astCSharp.CompilationUnit;
        	
        	var currentUsingDeclarations = new List<string>();
        	foreach(var usingDeclaration in astCSharp.AstDetails.UsingDeclarations)
        		currentUsingDeclarations.Add(usingDeclaration.Text);
        	
        	var defaultUsingStatements = getDefaultUsingStatements();
        	foreach(var usingStatement in defaultUsingStatements)
        		if (false == currentUsingDeclarations.Contains(usingStatement))
        			compilationUnit.add_Using(usingStatement);

            foreach (var comment in astCSharp.AstDetails.Comments)
            {
                comment.Text.starts("using ", false, value => astCSharp.CompilationUnit.add_Using(value));
                comment.Text.starts(new [] {"ref ", "O2Ref:"}, false,  value => ReferencedAssemblies.Add(value));
                comment.Text.starts(new[] { "file ", "O2File:" }, false, value => ExtraSourceCodeFilesToCompile.Add(value)); 
               
                comment.Text.starts(new[] {"O2:debugSymbols",
                                        "generateDebugSymbols", 
                                        "debugSymbols"}, true, (value) => generateDebugSymbols = true);
                comment.Text.eq("StaThread", () => { ExecuteInStaThread = true; });
                comment.Text.eq("MtaThread", () => { ExecuteInMtaThread = true; });  
            }            
        }               
        
        public object executeFirstMethod()
        {        	
        	var parametersValues = InvocationParameters.valuesArray();
        	return executeFirstMethod(parametersValues);
        }
        
        public object executeFirstMethod(params object[] parameters)
        {
        	var assembly = CompilerResults.CompiledAssembly;
        	if (assembly != null)
        	{
        		var methods = assembly.methods();
                foreach(var method in methods)
                    if (method.IsSpecialName == false)  // we need to do this since Properties get_ and set_ also look like methods
        		//if (methods.Count >0)        		
        		//{
        		    {
                        if (ExecuteInStaThread)
                            return O2Thread.staThread(() => executeMethod(method, parameters));
                        if (ExecuteInMtaThread)
                            return O2Thread.mtaThread(() => executeMethod(method, parameters));
                        return executeMethod(method, parameters);
        			    
        		    }
        	}
        	return null;
        }       
        
        public object executeMethod(MethodInfo method, params object[] parameters)
        {
            try
            {
                if (method.parameters().size() == parameters.size())
                    return method.invoke(parameters);
                return method.invoke();
            }
            catch (Exception ex)
            {
                ex.log("in CSharp_FastCompiler.executeMethod");
                return null;
            }
        }

        /*public string processedCode()
        {
            //if (OriginalCodeSnippet.valid())
            //    return OriginalCodeSnippet;
            return SourceCode;                    
        }*/

        public Location getGeneratedSourceCodeMethodLineOffset()
        {
            if (CreatedFromSnipptet == true && SourceCode.valid())
                //if (OriginalCodeSnippet != SourceCode)      // if they are the same it means that there is no offset                    
                    if (AstDetails.Methods.size() > 0)
                    {
                        return AstDetails.Methods[0].OriginalObject.firstLineOfCode();

                        /*var firstMethod = AstDetails.Methods.first<AstValue>();
                        if (firstMethod.StartLocation.Line != 0)
                        {

//                            var methodDeclaration = AstDetails.Methods.first<AstValue>().OriginalObject.cast<MethodDeclaration>();
                            var methodDeclaration = (MethodDeclaration)AstDetails.Methods[0].OriginalObject;
                            var firstInstruction = methodDeclaration.Body.Children[0].StartLocation; ;
//                            methodDeclaration.
                            //var aa = aaa.typeFullName();
                         //   return AstDetails.Methods.first<AstValue>().StartLocation;
                            //var location = AstDetails.Methods.first<AstValue>().StartLocation;
                            return new Location(firstInstruction.Column - 1, firstInstruction.Line - 1);
                        }*/
                    }
            return new Location(0, 0) ;
        }
    }
}
