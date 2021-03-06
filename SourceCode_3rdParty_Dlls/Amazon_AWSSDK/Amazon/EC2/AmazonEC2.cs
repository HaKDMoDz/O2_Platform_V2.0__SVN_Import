﻿namespace Amazon.EC2
{
    using Amazon.EC2.Model;
    using System;

    public interface AmazonEC2 : IDisposable
    {
        AllocateAddressResponse AllocateAddress(AllocateAddressRequest request);
        AssociateAddressResponse AssociateAddress(AssociateAddressRequest request);
        AssociateDhcpOptionsResponse AssociateDhcpOptions(AssociateDhcpOptionsRequest request);
        AttachVolumeResponse AttachVolume(AttachVolumeRequest request);
        AttachVpnGatewayResponse AttachVpnGateway(AttachVpnGatewayRequest request);
        AuthorizeSecurityGroupIngressResponse AuthorizeSecurityGroupIngress(AuthorizeSecurityGroupIngressRequest request);
        BundleInstanceResponse BundleInstance(BundleInstanceRequest request);
        CancelBundleTaskResponse CancelBundleTask(CancelBundleTaskRequest request);
        CancelSpotInstanceRequestsResponse CancelSpotInstanceRequests(CancelSpotInstanceRequestsRequest request);
        ConfirmProductInstanceResponse ConfirmProductInstance(ConfirmProductInstanceRequest request);
        CreateCustomerGatewayResponse CreateCustomerGateway(CreateCustomerGatewayRequest request);
        CreateDhcpOptionsResponse CreateDhcpOptions(CreateDhcpOptionsRequest request);
        CreateImageResponse CreateImage(CreateImageRequest request);
        CreateKeyPairResponse CreateKeyPair(CreateKeyPairRequest request);
        CreateSecurityGroupResponse CreateSecurityGroup(CreateSecurityGroupRequest request);
        CreateSnapshotResponse CreateSnapshot(CreateSnapshotRequest request);
        CreateSpotDatafeedSubscriptionResponse CreateSpotDatafeedSubscription(CreateSpotDatafeedSubscriptionRequest request);
        CreateSubnetResponse CreateSubnet(CreateSubnetRequest request);
        CreateVolumeResponse CreateVolume(CreateVolumeRequest request);
        CreateVpcResponse CreateVpc(CreateVpcRequest request);
        CreateVpnConnectionResponse CreateVpnConnection(CreateVpnConnectionRequest request);
        CreateVpnGatewayResponse CreateVpnGateway(CreateVpnGatewayRequest request);
        DeleteCustomerGatewayResponse DeleteCustomerGateway(DeleteCustomerGatewayRequest request);
        DeleteDhcpOptionsResponse DeleteDhcpOptions(DeleteDhcpOptionsRequest request);
        DeleteKeyPairResponse DeleteKeyPair(DeleteKeyPairRequest request);
        DeleteSecurityGroupResponse DeleteSecurityGroup(DeleteSecurityGroupRequest request);
        DeleteSnapshotResponse DeleteSnapshot(DeleteSnapshotRequest request);
        DeleteSpotDatafeedSubscriptionResponse DeleteSpotDatafeedSubscription(DeleteSpotDatafeedSubscriptionRequest request);
        DeleteSubnetResponse DeleteSubnet(DeleteSubnetRequest request);
        DeleteVolumeResponse DeleteVolume(DeleteVolumeRequest request);
        DeleteVpcResponse DeleteVpc(DeleteVpcRequest request);
        DeleteVpnConnectionResponse DeleteVpnConnection(DeleteVpnConnectionRequest request);
        DeleteVpnGatewayResponse DeleteVpnGateway(DeleteVpnGatewayRequest request);
        DeregisterImageResponse DeregisterImage(DeregisterImageRequest request);
        DescribeAddressesResponse DescribeAddresses(DescribeAddressesRequest request);
        DescribeAvailabilityZonesResponse DescribeAvailabilityZones(DescribeAvailabilityZonesRequest request);
        DescribeBundleTasksResponse DescribeBundleTasks(DescribeBundleTasksRequest request);
        DescribeCustomerGatewaysResponse DescribeCustomerGateways(DescribeCustomerGatewaysRequest request);
        DescribeDhcpOptionsResponse DescribeDhcpOptions(DescribeDhcpOptionsRequest request);
        DescribeImageAttributeResponse DescribeImageAttribute(DescribeImageAttributeRequest request);
        DescribeImagesResponse DescribeImages(DescribeImagesRequest request);
        DescribeInstanceAttributeResponse DescribeInstanceAttribute(DescribeInstanceAttributeRequest request);
        DescribeInstancesResponse DescribeInstances(DescribeInstancesRequest request);
        DescribeKeyPairsResponse DescribeKeyPairs(DescribeKeyPairsRequest request);
        DescribeRegionsResponse DescribeRegions(DescribeRegionsRequest request);
        DescribeReservedInstancesResponse DescribeReservedInstances(DescribeReservedInstancesRequest request);
        DescribeReservedInstancesOfferingsResponse DescribeReservedInstancesOfferings(DescribeReservedInstancesOfferingsRequest request);
        DescribeSecurityGroupsResponse DescribeSecurityGroups(DescribeSecurityGroupsRequest request);
        DescribeSnapshotAttributeResponse DescribeSnapshotAttribute(DescribeSnapshotAttributeRequest request);
        DescribeSnapshotsResponse DescribeSnapshots(DescribeSnapshotsRequest request);
        DescribeSpotDatafeedSubscriptionResponse DescribeSpotDatafeedSubscription(DescribeSpotDatafeedSubscriptionRequest request);
        DescribeSpotInstanceRequestsResponse DescribeSpotInstanceRequests(DescribeSpotInstanceRequestsRequest request);
        DescribeSpotPriceHistoryResponse DescribeSpotPriceHistory(DescribeSpotPriceHistoryRequest request);
        DescribeSubnetsResponse DescribeSubnets(DescribeSubnetsRequest request);
        DescribeVolumesResponse DescribeVolumes(DescribeVolumesRequest request);
        DescribeVpcsResponse DescribeVpcs(DescribeVpcsRequest request);
        DescribeVpnConnectionsResponse DescribeVpnConnections(DescribeVpnConnectionsRequest request);
        DescribeVpnGatewaysResponse DescribeVpnGateways(DescribeVpnGatewaysRequest request);
        DetachVolumeResponse DetachVolume(DetachVolumeRequest request);
        DetachVpnGatewayResponse DetachVpnGateway(DetachVpnGatewayRequest request);
        DisassociateAddressResponse DisassociateAddress(DisassociateAddressRequest request);
        GetConsoleOutputResponse GetConsoleOutput(GetConsoleOutputRequest request);
        GetPasswordDataResponse GetPasswordData(GetPasswordDataRequest request);
        ModifyImageAttributeResponse ModifyImageAttribute(ModifyImageAttributeRequest request);
        ModifyInstanceAttributeResponse ModifyInstanceAttribute(ModifyInstanceAttributeRequest request);
        ModifySnapshotAttributeResponse ModifySnapshotAttribute(ModifySnapshotAttributeRequest request);
        MonitorInstancesResponse MonitorInstances(MonitorInstancesRequest request);
        PurchaseReservedInstancesOfferingResponse PurchaseReservedInstancesOffering(PurchaseReservedInstancesOfferingRequest request);
        RebootInstancesResponse RebootInstances(RebootInstancesRequest request);
        RegisterImageResponse RegisterImage(RegisterImageRequest request);
        ReleaseAddressResponse ReleaseAddress(ReleaseAddressRequest request);
        RequestSpotInstancesResponse RequestSpotInstances(RequestSpotInstancesRequest request);
        ResetImageAttributeResponse ResetImageAttribute(ResetImageAttributeRequest request);
        ResetInstanceAttributeResponse ResetInstanceAttribute(ResetInstanceAttributeRequest request);
        ResetSnapshotAttributeResponse ResetSnapshotAttribute(ResetSnapshotAttributeRequest request);
        RevokeSecurityGroupIngressResponse RevokeSecurityGroupIngress(RevokeSecurityGroupIngressRequest request);
        RunInstancesResponse RunInstances(RunInstancesRequest request);
        StartInstancesResponse StartInstances(StartInstancesRequest request);
        StopInstancesResponse StopInstances(StopInstancesRequest request);
        TerminateInstancesResponse TerminateInstances(TerminateInstancesRequest request);
        UnmonitorInstancesResponse UnmonitorInstances(UnmonitorInstancesRequest request);
    }
}

