#pragma checksum "D:\Projects\veelki.com\Veelki.Admin\Veelki.Admin\Views\Setting\_seriesList.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "c5bee3dcc3b4da582d57115691fcaa366469629d"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Setting__seriesList), @"mvc.1.0.view", @"/Views/Setting/_seriesList.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"c5bee3dcc3b4da582d57115691fcaa366469629d", @"/Views/Setting/_seriesList.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"79c6561a5a268df3ed5470800eadc04f69a403d9", @"/Views/_ViewImports.cshtml")]
    public class Views_Setting__seriesList : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<Veelki.Data.Entities.Series>>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral(@"
<table class=""sportesTbl table01 margin-table"" style=""text-align:center;"">
    <thead>
        <tr>
            <th width=""5%"">S NO.</th>
            <th>Tournament Name</th>
            <th width=""15%"">ACTION</th>
        </tr>
    </thead>
    <tbody>
");
#nullable restore
#line 12 "D:\Projects\veelki.com\Veelki.Admin\Veelki.Admin\Views\Setting\_seriesList.cshtml"
         if (Model.Count() > 0)
        {
            int i = 1;
            foreach (var item in Model)
            {

#line default
#line hidden
#nullable disable
            WriteLiteral("                <tr>\r\n                    <td scope=\"row\">");
#nullable restore
#line 18 "D:\Projects\veelki.com\Veelki.Admin\Veelki.Admin\Views\Setting\_seriesList.cshtml"
                               Write(i);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                    <td>\r\n                        ");
#nullable restore
#line 20 "D:\Projects\veelki.com\Veelki.Admin\Veelki.Admin\Views\Setting\_seriesList.cshtml"
                   Write(item.tournamentName);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                    </td>\r\n                    <td>\r\n                        <input");
            BeginWriteAttribute("id", " id=\"", 660, "\"", 698, 2);
            WriteAttributeValue("", 665, "tournamentName_", 665, 15, true);
#nullable restore
#line 23 "D:\Projects\veelki.com\Veelki.Admin\Veelki.Admin\Views\Setting\_seriesList.cshtml"
WriteAttributeValue("", 680, item.tournamentId, 680, 18, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" type=\"hidden\"");
            BeginWriteAttribute("value", "  value=\"", 713, "\"", 742, 1);
#nullable restore
#line 23 "D:\Projects\veelki.com\Veelki.Admin\Veelki.Admin\Views\Setting\_seriesList.cshtml"
WriteAttributeValue("", 722, item.tournamentName, 722, 20, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral("/>\r\n                        <a href=\"javascript:void(0)\"");
            BeginWriteAttribute("onchange", " onchange=\"", 799, "\"", 857, 5);
            WriteAttributeValue("", 810, "UpdateSeries(", 810, 13, true);
#nullable restore
#line 24 "D:\Projects\veelki.com\Veelki.Admin\Veelki.Admin\Views\Setting\_seriesList.cshtml"
WriteAttributeValue("", 823, item.tournamentId, 823, 18, false);

#line default
#line hidden
#nullable disable
            WriteAttributeValue("", 841, ",", 841, 1, true);
#nullable restore
#line 24 "D:\Projects\veelki.com\Veelki.Admin\Veelki.Admin\Views\Setting\_seriesList.cshtml"
WriteAttributeValue("", 842, item.SportId, 842, 13, false);

#line default
#line hidden
#nullable disable
            WriteAttributeValue("", 855, ");", 855, 2, true);
            EndWriteAttribute();
            WriteLiteral(@">
                            <div class=""row"">
                                <div class=""col-3"">
                                    <span class=""switch switch-success"">
                                        <label>
                                            <input type=""checkbox""");
            BeginWriteAttribute("checked", " checked=\"", 1150, "\"", 1172, 1);
#nullable restore
#line 29 "D:\Projects\veelki.com\Veelki.Admin\Veelki.Admin\Views\Setting\_seriesList.cshtml"
WriteAttributeValue("", 1160, item.Status, 1160, 12, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" name=\"select\"");
            BeginWriteAttribute("id", " id=\"", 1187, "\"", 1225, 2);
            WriteAttributeValue("", 1192, "checkBoxAction_", 1192, 15, true);
#nullable restore
#line 29 "D:\Projects\veelki.com\Veelki.Admin\Veelki.Admin\Views\Setting\_seriesList.cshtml"
WriteAttributeValue("", 1207, item.tournamentId, 1207, 18, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(@" />
                                            <span></span>
                                        </label>
                                    </span>
                                </div>
                            </div>
                        </a>
                    </td>
                </tr>
");
#nullable restore
#line 38 "D:\Projects\veelki.com\Veelki.Admin\Veelki.Admin\Views\Setting\_seriesList.cshtml"
                i++;
            }
        }

#line default
#line hidden
#nullable disable
            WriteLiteral("    </tbody>\r\n</table>");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<Veelki.Data.Entities.Series>> Html { get; private set; }
    }
}
#pragma warning restore 1591
