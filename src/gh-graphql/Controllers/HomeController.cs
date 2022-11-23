using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using gh_graphql.Models;
using GraphQL.Client.Abstractions;
using GraphQL;
using GraphQL.Client.Http;
using GraphQL.Client.Serializer.Newtonsoft;
using RestSharp;
using Newtonsoft.Json;
using System.Net;
using System.Text;
using System;

namespace gh_graphql.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Result ()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Index(InputModel model)
    {
        try
        {
            if (ModelState.IsValid)
            {
                Result myDeserializedClass = Consultar(model.organization, model.repo, model.token );
                return View("Result", myDeserializedClass);
            }

            return View(model);
        }
        catch
        {
            return Redirect("/Home/Error");
        }
    }

    private Result Consultar(string org, string repo, string token)
    {
        var query = "{\"query\":\"query MyQuery {\\r\\n  repository(owner: \\\"" + org + "\\\", name: \\\""+ repo +"\\\") {\\r\\n  id\\r\\n   url\\r\\n  name\\r\\n  createdAt\\r\\n  allowUpdateBranch\\r\\n  defaultBranchRef {\\r\\n  name\\r\\n }\\r\\n isInOrganization\\r\\n autoMergeAllowed\\r\\n  licenseInfo {\\r\\n  name\\r\\n  }\\r\\n  dependencyGraphManifests(first: 10, withDependencies: true) {\\r\\n  nodes {\\r\\n   blobPath\\r\\n    dependencies {\\r\\n  nodes {\\r\\n   packageName\\r\\n  requirements\\r\\n  hasDependencies\\r\\n  repository {\\r\\n   name\\r\\n licenseInfo {\\r\\n  name\\r\\n   }\\r\\n  nameWithOwner\\r\\n  owner {\\r\\n login\\r\\n }\\r\\n  primaryLanguage {\\r\\n name\\r\\n  }\\r\\n  }\\r\\n }\\r\\n }\\r\\n }\\r\\n }\\r\\n  }\\r\\n }\\r\\n\"}";

        var client = new RestClient("https://api.github.com/graphql");
        var request = new RestRequest();
        string authorization = "bearer " + token;
        request.AddHeader("Authorization", authorization);
        request.AddHeader("Accept", "application/vnd.github.hawkgirl-preview+json");
        request.AddHeader("Content-Type", "application/json");
        request.AddParameter("application/json", query, ParameterType.RequestBody);
        var response = client.Post(request);

        Result myDeserializedClass = JsonConvert.DeserializeObject<Result>(response.Content);
        return myDeserializedClass;
    }
}
