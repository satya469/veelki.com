#pragma checksum "D:\Projects\veelki.com\Veelki.Admin\Veelki.Admin\Views\Account\MyAccount.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "bd953bedd1e7f1b362445a9281fb583743508b67"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Account_MyAccount), @"mvc.1.0.view", @"/Views/Account/MyAccount.cshtml")]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 1 "D:\Projects\veelki.com\Veelki.Admin\Veelki.Admin\Views\_ViewImports.cshtml"
using Veelki.Admin;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "D:\Projects\veelki.com\Veelki.Admin\Veelki.Admin\Views\_ViewImports.cshtml"
using Veelki.Admin.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"bd953bedd1e7f1b362445a9281fb583743508b67", @"/Views/Account/MyAccount.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"79c6561a5a268df3ed5470800eadc04f69a403d9", @"/Views/_ViewImports.cshtml")]
    public class Views_Account_MyAccount : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 2 "D:\Projects\veelki.com\Veelki.Admin\Veelki.Admin\Views\Account\MyAccount.cshtml"
  
    ViewData["Title"] = "My Account";
    Layout = "~/Views/Shared/_Layout.cshtml";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<div class=\"main_wrap\">\r\n    <!-- agent path -->\r\n\r\n\r\n    <div class=\"agent_path\">\r\n        <ul id=\"agentPath\" class=\"agent_path-L\">\r\n\r\n            <li id=\"path6\"");
            BeginWriteAttribute("class", " class=\"", 259, "\"", 267, 0);
            EndWriteAttribute();
            WriteLiteral(@" style=""display: none;"">
                <a href=""javascript: void(0);"">
                    <span class=""lv_0"">
                        COM
                    </span>
                    <strong></strong>
                </a>
            </li>

            <li id=""path5""");
            BeginWriteAttribute("class", " class=\"", 550, "\"", 558, 0);
            EndWriteAttribute();
            WriteLiteral(@" style=""display: none;"">
                <a href=""javascript: void(0);"">
                    <span class=""lv_1"">
                        SS
                    </span>
                    <strong></strong>
                </a>
            </li>

            <li id=""path4""");
            BeginWriteAttribute("class", " class=\"", 840, "\"", 848, 0);
            EndWriteAttribute();
            WriteLiteral(@" style=""display: none;"">
                <a href=""javascript: void(0);"">
                    <span class=""lv_2"">
                        SUP
                    </span>
                    <strong></strong>
                </a>
            </li>

");
#nullable restore
#line 41 "D:\Projects\veelki.com\Veelki.Admin\Veelki.Admin\Views\Account\MyAccount.cshtml"
              
                string role = "";
                if (ViewBag.LoginUser.RoleId == 2)
                {
                    role = "SA";
                }
                else if (ViewBag.LoginUser.RoleId == 3)
                {
                    role = "AD";
                }
                else if (ViewBag.LoginUser.RoleId == 4)
                {
                    role = "SUA";
                }
                else if (ViewBag.LoginUser.RoleId == 5)
                {
                    role = "SUM";
                }
                else if (ViewBag.LoginUser.RoleId == 6)
                {
                    role = "MA";
                }

#line default
#line hidden
#nullable disable
            WriteLiteral("                <li id=\"path3\" class=\"last_li\">\r\n                    <a href=\"javascript: void(0);\">\r\n                        <span class=\"lv_3\">\r\n                            ");
#nullable restore
#line 66 "D:\Projects\veelki.com\Veelki.Admin\Veelki.Admin\Views\Account\MyAccount.cshtml"
                       Write(role);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                        </span>\r\n                        <strong>");
#nullable restore
#line 68 "D:\Projects\veelki.com\Veelki.Admin\Veelki.Admin\Views\Account\MyAccount.cshtml"
                           Write(ViewBag.LoginUser.UserName);

#line default
#line hidden
#nullable disable
            WriteLiteral("</strong>\r\n                    </a>\r\n                </li>\r\n");
            WriteLiteral("\r\n            <li id=\"path0\"");
            BeginWriteAttribute("class", " class=\"", 2175, "\"", 2183, 0);
            EndWriteAttribute();
            WriteLiteral(@" style=""display: none;"">
                <a href=""javascript: void(0);"">
                    <span class=""lv_4"">
                        PL
                    </span>
                    <strong></strong>
                </a>
            </li>

            <ul class=""account_pop"" id=""accountPop"">
                <li id=""popTmp"" style=""display: none;"">
                    <a");
            BeginWriteAttribute("href", " href=\"", 2572, "\"", 2579, 0);
            EndWriteAttribute();
            WriteLiteral(@"></a>
                </li>
            </ul>
        </ul>
    </div>

    <!-- Agent Left Column -->

    <div class=""col-left"">

        <!-- Sub Menu and Path -->
        <div class=""sub_path"">

            <!-- Sub Menu -->
            <ul class=""menu-list"">

                <li class=""class"">
                    Position
                </li>


                <li>
                    <a id=""accountStatement"" href=""acount-deatil.html"">
                        Account Statement
                    </a>
                </li>


                <li>
                    <a id=""accountSummary"" href=""#"" class=""select"">
                        Account Summary
                    </a>
                </li>



                <li class=""class"">
                    Account Details
                </li>
                <li>
                    <a id=""profile"" href=""profile.html"">
                        Profile
                    </a>
                </li>
              ");
            WriteLiteral(@"  <li>
                    <a id=""activityLog"" href=""active-log.html"">
                        Activity Log
                    </a>
                </li>


            </ul>
        </div>
    </div>




    <!-- Loading Wrap -->
    <div id=""loading"" class=""loading-wrap"" style=""display:none"">
        <ul class=""loading"">
            <li><img src=""https://statics3.skyexch.bike/images/loading40.gif""></li>
            <li>
                Loading...
            </li>
            <br>
            <li><span id=""progress""></span></li>
        </ul>
    </div>

    <!-- Message -->
    <div id=""message"" class=""message-wrap success"">
        <a class=""btn-close"">Close</a>
        <p></p>
    </div>

    <!-- Center Column -->
    <div class=""col-center report"">

        <h2>
            Account Summary
        </h2>

        <div class=""white-wrap"">
            <dl class=""head-balance"">
                <dt>
                    Your Balances
                </dt>
         ");
            WriteLiteral("       <dd id=\"yourBalance\">0.00 <span>PTH</span></dd>\r\n            </dl>\r\n\r\n\r\n\r\n\r\n        </div>\r\n    </div>\r\n</div>");
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; }
    }
}
#pragma warning restore 1591
