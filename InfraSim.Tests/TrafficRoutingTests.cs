using System.Collections.Generic;
using Xunit;
using Moq;


public class TrafficRoutingTests
{
    [Fact] 
    public void TestRequestCount_ShouldReturnCorrectRequestCount()
    {
        // Arrange
        TrafficRouting trafficRouting = new TrafficRouting(new List<IServer>());

        // Act
        int result = trafficRouting.CalculateRequests(100);

        // Assert
        Assert.Equal(100, result);
    }

    [Fact]
    public void TestRequestCount_ShouldReturnCorrectValue()
    {
        TrafficRouting trafficRouting = new TrafficRouting(new List<IServer>());
        Assert.Equal(50, trafficRouting.CalculateRequests(50));
        Assert.Equal(200, trafficRouting.CalculateRequests(200));
    }

    [Fact]
    public void TestSendRequestsToServers_ShouldDistributeRequestsEvenly()
    {
        // Arrange
        var mockServer1 = new Mock<IServer>();
        var mockServer2 = new Mock<IServer>();
        var mockServer3 = new Mock<IServer>();
        List<IServer> servers = new List<IServer> { mockServer1.Object, mockServer2.Object, mockServer3.Object };
        TrafficRouting trafficRouting = new TrafficRouting(servers);

        // Act
        trafficRouting.SendRequestsToServers(100, servers);

        // Assert (each server gets 33, one gets 34)
        mockServer1.Verify(s => s.HandleRequests(It.IsInRange(33, 34, Moq.Range.Inclusive)), Times.Once);
        mockServer2.Verify(s => s.HandleRequests(It.IsInRange(33, 34, Moq.Range.Inclusive)), Times.Once);
        mockServer3.Verify(s => s.HandleRequests(It.IsInRange(33, 34, Moq.Range.Inclusive)), Times.Once);
    }

    [Fact]
    public void ObtainServers_ShouldReturnCorrectServers()
    {
        // Arrange
        var servers = new List<IServer> { new Mock<IServer>().Object, new Mock<IServer>().Object };
        var trafficRouting = new TrafficRouting(servers);

        // Act
        var result = trafficRouting.ObtainServers();

        // Assert
        Assert.Equal(servers.Count, result.Count);
    }   
}
