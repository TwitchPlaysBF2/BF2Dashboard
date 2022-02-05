using System.Collections.Generic;
using BF2Dashboard.Domain.BattlefieldApi;
using BF2Dashboard.Domain.Repositories;
using NUnit.Framework;

namespace BF2Dashboard.Domain.Tests.Repositories;

[TestFixture]
public class PagedServerListResponseTests
{
    [Test]
    public void IsLastPage_WithOutServers_HasNoKnownMaxPageInfo_IsFalse()
    {
        var model = new PagedServerListResponse(new List<Server>(), 1, null);

        Assert.That(model.IsLastPage, Is.False);
    }

    [Test]
    public void IsLastPage_WithServers_HasNoKnownMaxPageInfo_IsFalse()
    {
        var model = new PagedServerListResponse(new List<Server> {new()}, 1, null);

        Assert.That(model.IsLastPage, Is.False);
    }

    [TestCase(0, "2")]
    [TestCase(1, "2")]
    public void IsLastPage_HasKnownMaxPageInfo_IsLower_IsFalse(int currentPage, string maxPage)
    {
        var model = new PagedServerListResponse(new List<Server>(), currentPage, maxPage);

        Assert.That(model.IsLastPage, Is.False);
    }

    [TestCase(2, "2")]
    [TestCase(3, "2")]
    public void IsLastPage_HasKnownMaxPageInfo_IsSameOrHigher_IsTrue(int currentPage, string maxPage)
    {
        var model = new PagedServerListResponse(new List<Server>(), currentPage, maxPage);

        Assert.That(model.IsLastPage, Is.True);
    }
}