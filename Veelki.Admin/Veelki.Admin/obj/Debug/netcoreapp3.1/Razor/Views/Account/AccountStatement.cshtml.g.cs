#pragma checksum "D:\Projects\veelki.com\Veelki.Admin\Veelki.Admin\Views\Account\AccountStatement.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "1ee4ad75c62afddcf231a81f37f4286bc2ae622e"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Account_AccountStatement), @"mvc.1.0.view", @"/Views/Account/AccountStatement.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"1ee4ad75c62afddcf231a81f37f4286bc2ae622e", @"/Views/Account/AccountStatement.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"79c6561a5a268df3ed5470800eadc04f69a403d9", @"/Views/_ViewImports.cshtml")]
    public class Views_Account_AccountStatement : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<List<Veelki.Model.ViewModel.AccountStatementVM>>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 2 "D:\Projects\veelki.com\Veelki.Admin\Veelki.Admin\Views\Account\AccountStatement.cshtml"
  
    ViewData["Title"] = "AccountStatement";
    Layout = "~/Views/Shared/_AdminLayout.cshtml";

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
<div class=""card card-custom"">
    <div class=""card-header flex-wrap py-5"">
        <div class=""card-title"">
            <h3 class=""card-label"">
                Account Statement
            </h3>
        </div>

    </div>
    <div class=""card-body"">
        <!--begin: Datatable-->
        <table class=""table table-striped table-bordered"" id=""Ac_datatable"">
            <thead >
                <tr>
                    <th>
                        Date/Time
                    </th>
                    <th>
                        Deposit
                    </th>
                    <th>
                        Withdraw
                    </th>
                    <th>
                        Balance
                    </th>
                    <th>
                        Remark
                    </th>
                    <th>
                        From -> To
                    </th>
                </tr>
            </thead>
            <tbody>
");
#nullable restore
#line 42 "D:\Projects\veelki.com\Veelki.Admin\Veelki.Admin\Views\Account\AccountStatement.cshtml"
                 if (Model != null && Model.Count() > 0)
                {
                    foreach (var item in Model)
                    {

#line default
#line hidden
#nullable disable
            WriteLiteral("                        <tr>\r\n                            <td>");
#nullable restore
#line 47 "D:\Projects\veelki.com\Veelki.Admin\Veelki.Admin\Views\Account\AccountStatement.cshtml"
                           Write(item.CreatedDate);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                            <td><label>");
#nullable restore
#line 48 "D:\Projects\veelki.com\Veelki.Admin\Veelki.Admin\Views\Account\AccountStatement.cshtml"
                                   Write(item.Deposit > 0 ? item.Deposit.ToString() : "-");

#line default
#line hidden
#nullable disable
            WriteLiteral("</label></td>\r\n                            <td><label style=\"color:red;\">");
#nullable restore
#line 49 "D:\Projects\veelki.com\Veelki.Admin\Veelki.Admin\Views\Account\AccountStatement.cshtml"
                                                      Write(item.Withdraw > 0 ? "(" + item.Withdraw.ToString() + ")" : "(0)");

#line default
#line hidden
#nullable disable
            WriteLiteral("</label></td>\r\n                            <td><label>");
#nullable restore
#line 50 "D:\Projects\veelki.com\Veelki.Admin\Veelki.Admin\Views\Account\AccountStatement.cshtml"
                                  Write(item.Balance);

#line default
#line hidden
#nullable disable
            WriteLiteral("</label></td>\r\n                            <td>");
#nullable restore
#line 51 "D:\Projects\veelki.com\Veelki.Admin\Veelki.Admin\Views\Account\AccountStatement.cshtml"
                           Write(item.Remark);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                            <td>");
#nullable restore
#line 52 "D:\Projects\veelki.com\Veelki.Admin\Veelki.Admin\Views\Account\AccountStatement.cshtml"
                           Write(item.FromUser);

#line default
#line hidden
#nullable disable
            WriteLiteral("<b> -> </b>");
#nullable restore
#line 52 "D:\Projects\veelki.com\Veelki.Admin\Veelki.Admin\Views\Account\AccountStatement.cshtml"
                                                    Write(item.ToUser);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                        </tr>\r\n");
#nullable restore
#line 54 "D:\Projects\veelki.com\Veelki.Admin\Veelki.Admin\Views\Account\AccountStatement.cshtml"
                    }
                }

#line default
#line hidden
#nullable disable
            WriteLiteral("            </tbody>\r\n        </table>\r\n    </div>\r\n</div>\r\n\r\n");
            DefineSection("PageJs", async() => {
                WriteLiteral(@"
    <script src=""https://cdn.datatables.net/1.11.0/js/jquery.dataTables.min.js""></script>
    <script src=""https://cdn.datatables.net/1.11.0/js/dataTables.bootstrap.min.js""></script>
    <script type=""text/javascript"">
        $(function () {
            $(""#Ac_datatable"").DataTable();
            $("".dataTables_length"").addClass(""bs-select"");
        });
    </script>
");
            }
            );
            WriteLiteral("\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<List<Veelki.Model.ViewModel.AccountStatementVM>> Html { get; private set; }
    }
}
#pragma warning restore 1591
