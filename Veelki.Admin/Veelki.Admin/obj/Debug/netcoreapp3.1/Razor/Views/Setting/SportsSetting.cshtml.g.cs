#pragma checksum "D:\Projects\veelki.com\Veelki.Admin\Veelki.Admin\Views\Setting\SportsSetting.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "ba8ac15db07595818bb6952c7453bd36708888b7"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Setting_SportsSetting), @"mvc.1.0.view", @"/Views/Setting/SportsSetting.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"ba8ac15db07595818bb6952c7453bd36708888b7", @"/Views/Setting/SportsSetting.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"79c6561a5a268df3ed5470800eadc04f69a403d9", @"/Views/_ViewImports.cshtml")]
    public class Views_Setting_SportsSetting : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 2 "D:\Projects\veelki.com\Veelki.Admin\Veelki.Admin\Views\Setting\SportsSetting.cshtml"
  
    ViewBag.Title = "SportsSetting";
    Layout = "~/Views/Shared/_AdminLayout.cshtml";

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
<div class=""container"">
    <h2>Sports Settings</h2>
    <table class=""sportesTbl table table-striped table-dark"">
        <thead>
            <tr>
                <th>S NO.</th>
                <th>SPORT</th>
                <th>ACTION</th>
                <th>HIGHLIGHT</th>
                <th>SETTING</th>
            </tr>
        </thead>
        <tbody>
");
#nullable restore
#line 20 "D:\Projects\veelki.com\Veelki.Admin\Veelki.Admin\Views\Setting\SportsSetting.cshtml"
             if (ViewBag.SportsList != null)
            {
                int i = 1;
                foreach (var item in ViewBag.SportsList)
                {

#line default
#line hidden
#nullable disable
            WriteLiteral("                    <tr>\r\n                        <td scope=\"row\">");
#nullable restore
#line 26 "D:\Projects\veelki.com\Veelki.Admin\Veelki.Admin\Views\Setting\SportsSetting.cshtml"
                                   Write(i);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                        <td>");
#nullable restore
#line 27 "D:\Projects\veelki.com\Veelki.Admin\Veelki.Admin\Views\Setting\SportsSetting.cshtml"
                       Write(item.SportName);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                        <td>\r\n                            <a data-value=\"");
#nullable restore
#line 29 "D:\Projects\veelki.com\Veelki.Admin\Veelki.Admin\Views\Setting\SportsSetting.cshtml"
                                      Write(i);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"""
                               href=""javascript:void(0)"" class=""btnEdit"">
                                <div class=""row"">
                                    <div class=""col-3"">
                                        <span class=""switch switch-success"">
                                            <label>
                                                <input type=""checkbox""");
            BeginWriteAttribute("checked", " checked=\"", 1228, "\"", 1250, 1);
#nullable restore
#line 35 "D:\Projects\veelki.com\Veelki.Admin\Veelki.Admin\Views\Setting\SportsSetting.cshtml"
WriteAttributeValue("", 1238, item.Status, 1238, 12, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" name=\"select\"");
            BeginWriteAttribute("id", " id=\"", 1265, "\"", 1285, 2);
            WriteAttributeValue("", 1270, "Status_", 1270, 7, true);
#nullable restore
#line 35 "D:\Projects\veelki.com\Veelki.Admin\Veelki.Admin\Views\Setting\SportsSetting.cshtml"
WriteAttributeValue("", 1277, item.Id, 1277, 8, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(@" disabled />
                                                <span></span>
                                            </label>
                                        </span>
                                    </div>
                                </div>
                            </a>
                        </td>
                        <td>
                            <a data-value=""");
#nullable restore
#line 44 "D:\Projects\veelki.com\Veelki.Admin\Veelki.Admin\Views\Setting\SportsSetting.cshtml"
                                      Write(i);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"""
                               href=""javascript:void(0)"" class=""btnEdit"">
                                <div class=""row"">
                                    <div class=""col-3"">
                                        <span class=""switch switch-success"">
                                            <label>
                                                <input type=""checkbox""");
            BeginWriteAttribute("checked", " checked=\"", 2077, "\"", 2102, 1);
#nullable restore
#line 50 "D:\Projects\veelki.com\Veelki.Admin\Veelki.Admin\Views\Setting\SportsSetting.cshtml"
WriteAttributeValue("", 2087, item.Highlight, 2087, 15, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" name=\"select\"");
            BeginWriteAttribute("id", " id=\"", 2117, "\"", 2140, 2);
            WriteAttributeValue("", 2122, "Highlight_", 2122, 10, true);
#nullable restore
#line 50 "D:\Projects\veelki.com\Veelki.Admin\Veelki.Admin\Views\Setting\SportsSetting.cshtml"
WriteAttributeValue("", 2132, item.Id, 2132, 8, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(@" disabled />
                                                <span></span>
                                            </label>
                                        </span>
                                    </div>
                                </div>
                            </a>
                        </td>
                        <td>
                            <button");
            BeginWriteAttribute("id", " id=\"", 2535, "\"", 2561, 2);
            WriteAttributeValue("", 2540, "btnShowModal_", 2540, 13, true);
#nullable restore
#line 59 "D:\Projects\veelki.com\Veelki.Admin\Veelki.Admin\Views\Setting\SportsSetting.cshtml"
WriteAttributeValue("", 2553, item.Id, 2553, 8, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" type=\"button\"\r\n                                    class=\"btn btn-primary btn-sm showModel\">\r\n                                Setting\r\n                            </button>\r\n\r\n                            <div class=\"modal fade\" tabindex=\"-1\"");
            BeginWriteAttribute("id", " id=\"", 2804, "\"", 2830, 2);
            WriteAttributeValue("", 2809, "settingModal_", 2809, 13, true);
#nullable restore
#line 64 "D:\Projects\veelki.com\Veelki.Admin\Veelki.Admin\Views\Setting\SportsSetting.cshtml"
WriteAttributeValue("", 2822, item.Id, 2822, 8, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(@"
                                 data-keyboard=""false"" data-backdrop=""static"">
                                <div class=""modal-dialog modal-md"">
                                    <div class=""modal-content"">
                                        <div class=""modal-header"">
                                            <h4 class=""modal-title"">Sport Setting</h4>
                                            <button");
            BeginWriteAttribute("id", " id=\"", 3254, "\"", 3280, 2);
            WriteAttributeValue("", 3259, "btnHideModal_", 3259, 13, true);
#nullable restore
#line 70 "D:\Projects\veelki.com\Veelki.Admin\Veelki.Admin\Views\Setting\SportsSetting.cshtml"
WriteAttributeValue("", 3272, item.Id, 3272, 8, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(@" type=""button"" class=""close hideModel"" data-dismiss=""modal"">
                                                ×
                                            </button>
                                        </div>
                                        <div class=""modal-body"">
                                            <input type=""hidden""");
            BeginWriteAttribute("id", " id=\"", 3627, "\"", 3650, 2);
            WriteAttributeValue("", 3632, "sportName_", 3632, 10, true);
#nullable restore
#line 75 "D:\Projects\veelki.com\Veelki.Admin\Veelki.Admin\Views\Setting\SportsSetting.cshtml"
WriteAttributeValue("", 3642, item.Id, 3642, 8, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            BeginWriteAttribute("value", " value=\"", 3651, "\"", 3674, 1);
#nullable restore
#line 75 "D:\Projects\veelki.com\Veelki.Admin\Veelki.Admin\Views\Setting\SportsSetting.cshtml"
WriteAttributeValue("", 3659, item.SportName, 3659, 15, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" />\r\n                                            <input type=\"hidden\"");
            BeginWriteAttribute("id", " id=\"", 3744, "\"", 3766, 2);
            WriteAttributeValue("", 3749, "sequence_", 3749, 9, true);
#nullable restore
#line 76 "D:\Projects\veelki.com\Veelki.Admin\Veelki.Admin\Views\Setting\SportsSetting.cshtml"
WriteAttributeValue("", 3758, item.Id, 3758, 8, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            BeginWriteAttribute("value", " value=\"", 3767, "\"", 3789, 1);
#nullable restore
#line 76 "D:\Projects\veelki.com\Veelki.Admin\Veelki.Admin\Views\Setting\SportsSetting.cshtml"
WriteAttributeValue("", 3775, item.Sequence, 3775, 14, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(@" />
                                            <div class=""form-group"">
                                                <label>Min Odd Limit</label>
                                                <input class=""form-control"" type=""number""
                                                       placeholder=""Min Odd Limit...""");
            BeginWriteAttribute("id", " id=\"", 4119, "\"", 4144, 2);
            WriteAttributeValue("", 4124, "text_minOdd_", 4124, 12, true);
#nullable restore
#line 80 "D:\Projects\veelki.com\Veelki.Admin\Veelki.Admin\Views\Setting\SportsSetting.cshtml"
WriteAttributeValue("", 4136, item.Id, 4136, 8, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            BeginWriteAttribute("value", " value=\"", 4145, "\"", 4170, 1);
#nullable restore
#line 80 "D:\Projects\veelki.com\Veelki.Admin\Veelki.Admin\Views\Setting\SportsSetting.cshtml"
WriteAttributeValue("", 4153, item.MinOddLimit, 4153, 17, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(@" />
                                            </div>
                                            <div class=""form-group"">
                                                <label>Max Odd Limit</label>
                                                <input class=""form-control"" placeholder=""Max Odd Limit...""
                                                       type=""number""");
            BeginWriteAttribute("id", " id=\"", 4552, "\"", 4577, 2);
            WriteAttributeValue("", 4557, "text_maxOdd_", 4557, 12, true);
#nullable restore
#line 85 "D:\Projects\veelki.com\Veelki.Admin\Veelki.Admin\Views\Setting\SportsSetting.cshtml"
WriteAttributeValue("", 4569, item.Id, 4569, 8, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            BeginWriteAttribute("value", " value=\"", 4578, "\"", 4603, 1);
#nullable restore
#line 85 "D:\Projects\veelki.com\Veelki.Admin\Veelki.Admin\Views\Setting\SportsSetting.cshtml"
WriteAttributeValue("", 4586, item.MaxOddLimit, 4586, 17, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(@" />
                                            </div>
                                            <div class=""form-group"">
                                                <label>Max Stack Limit</label>
                                                <input class=""form-control"" type=""number""
                                                       placeholder=""Max Stack Limit...""");
            BeginWriteAttribute("id", " id=\"", 4989, "\"", 5021, 2);
            WriteAttributeValue("", 4994, "text_maxStackLimit_", 4994, 19, true);
#nullable restore
#line 90 "D:\Projects\veelki.com\Veelki.Admin\Veelki.Admin\Views\Setting\SportsSetting.cshtml"
WriteAttributeValue("", 5013, item.Id, 5013, 8, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            BeginWriteAttribute("value", " value=\"", 5022, "\"", 5049, 1);
#nullable restore
#line 90 "D:\Projects\veelki.com\Veelki.Admin\Veelki.Admin\Views\Setting\SportsSetting.cshtml"
WriteAttributeValue("", 5030, item.MaxStackLimit, 5030, 19, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(@" />
                                            </div>
                                            <div class=""form-group"">
                                                <label>Bet Delay Time</label>
                                                <input class=""form-control"" placeholder=""Bet Delay Time""
                                                       type=""number""");
            BeginWriteAttribute("id", " id=\"", 5430, "\"", 5461, 2);
            WriteAttributeValue("", 5435, "text_betDelayTime_", 5435, 18, true);
#nullable restore
#line 95 "D:\Projects\veelki.com\Veelki.Admin\Veelki.Admin\Views\Setting\SportsSetting.cshtml"
WriteAttributeValue("", 5453, item.Id, 5453, 8, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            BeginWriteAttribute("value", " value=\"", 5462, "\"", 5488, 1);
#nullable restore
#line 95 "D:\Projects\veelki.com\Veelki.Admin\Veelki.Admin\Views\Setting\SportsSetting.cshtml"
WriteAttributeValue("", 5470, item.BetDelayTime, 5470, 18, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(@" />
                                            </div>
                                            <div class=""form-group"">
                                                <label>Action</label>
                                                <span class=""switch switch-success"">
                                                    <label>
                                                        <input type=""checkbox""");
            BeginWriteAttribute("checked", " checked=\"", 5912, "\"", 5934, 1);
#nullable restore
#line 101 "D:\Projects\veelki.com\Veelki.Admin\Veelki.Admin\Views\Setting\SportsSetting.cshtml"
WriteAttributeValue("", 5922, item.Status, 5922, 12, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" name=\"select\"");
            BeginWriteAttribute("id", " id=\"", 5949, "\"", 5977, 2);
            WriteAttributeValue("", 5954, "checkBoxStatus_", 5954, 15, true);
#nullable restore
#line 101 "D:\Projects\veelki.com\Veelki.Admin\Veelki.Admin\Views\Setting\SportsSetting.cshtml"
WriteAttributeValue("", 5969, item.Id, 5969, 8, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(@" />
                                                        <span></span>
                                                    </label>
                                                </span>
                                            </div>
                                            <div class=""form-group"">
                                                <label>Highlight</label>
                                                <span class=""switch switch-success"">
                                                    <label>
                                                        <input type=""checkbox""");
            BeginWriteAttribute("checked", " checked=\"", 6594, "\"", 6619, 1);
#nullable restore
#line 110 "D:\Projects\veelki.com\Veelki.Admin\Veelki.Admin\Views\Setting\SportsSetting.cshtml"
WriteAttributeValue("", 6604, item.Highlight, 6604, 15, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" name=\"select\"");
            BeginWriteAttribute("id", " id=\"", 6634, "\"", 6665, 2);
            WriteAttributeValue("", 6639, "checkBoxHighlight_", 6639, 18, true);
#nullable restore
#line 110 "D:\Projects\veelki.com\Veelki.Admin\Veelki.Admin\Views\Setting\SportsSetting.cshtml"
WriteAttributeValue("", 6657, item.Id, 6657, 8, false);

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
                                        <div class=""modal-footer"">
                                            <button type=""submit""");
            BeginWriteAttribute("id", " id=\"", 7094, "\"", 7117, 2);
            WriteAttributeValue("", 7099, "submitBtn_", 7099, 10, true);
#nullable restore
#line 117 "D:\Projects\veelki.com\Veelki.Admin\Veelki.Admin\Views\Setting\SportsSetting.cshtml"
WriteAttributeValue("", 7109, item.Id, 7109, 8, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" class=\"btn btn-primary submitBtn\">Submit</button>\r\n                                            <button type=\"button\"");
            BeginWriteAttribute("id", " id=\"", 7235, "\"", 7261, 2);
            WriteAttributeValue("", 7240, "btnHideModal_", 7240, 13, true);
#nullable restore
#line 118 "D:\Projects\veelki.com\Veelki.Admin\Veelki.Admin\Views\Setting\SportsSetting.cshtml"
WriteAttributeValue("", 7253, item.Id, 7253, 8, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(@" class=""btn btn-danger hideModel"">
                                                close
                                            </button>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </td>
                    </tr>
");
#nullable restore
#line 127 "D:\Projects\veelki.com\Veelki.Admin\Veelki.Admin\Views\Setting\SportsSetting.cshtml"
                    i++;
                }
            }

#line default
#line hidden
#nullable disable
            WriteLiteral("        </tbody>\r\n    </table>\r\n</div>\r\n");
            DefineSection("PageJs", async() => {
                WriteLiteral(@"
    <script type=""text/javascript"">
        $(document).ready(function () {
            $("".showModel"").click(function () {
                var id = $(this).attr(""id"").split(""_"")[1];
                $(""#settingModal_"" + id).removeClass(""fade"");
                $(""#settingModal_"" + id).modal('show');
            });

            $("".hideModel"").click(function () {
                var id = $(this).attr(""id"").split(""_"")[1];
                $(""#settingModal_"" + id).addClass(""fade"");
                $(""#settingModal_"" + id).modal('hide');
            });
          
            $("".submitBtn"").click(function () {
                const id = $(this).parents().eq(3).attr(""id"").split(""_"")[1];

                const minOddLimit = $(""#text_minOdd_"" + id).val();
                const maxOddLimit = $(""#text_maxOdd_"" + id).val();
                const sportName = $(""#sportName_"" + id).val();
                const sequence = $(""#sequence_"" + id).val();
                const status = document.getElement");
                WriteLiteral(@"ById(""checkBoxStatus_"" + id).checked;
                const highlight = document.getElementById(""checkBoxHighlight_"" + id).checked;
                let formData = new FormData();
                formData.append(""Id"", id);
                formData.append(""MinOddLimit"", minOddLimit);
                formData.append(""MaxOddLimit"", maxOddLimit);
                formData.append(""MaxStackLimit"", $(""#text_maxStackLimit_"" + id).val());
                formData.append(""BetDelayTime"", $(""#text_betDelayTime_"" + id).val());
                formData.append(""SportName"", sportName);
                formData.append(""Status"", status);
                formData.append(""Highlight"", highlight);
                formData.append(""Sequence"", sequence);

                let addRequestJson = {
                apiUrl: """);
#nullable restore
#line 169 "D:\Projects\veelki.com\Veelki.Admin\Veelki.Admin\Views\Setting\SportsSetting.cshtml"
                    Write(Url.Action("SportsSetting", "Setting"));

#line default
#line hidden
#nullable disable
                WriteLiteral(@""",
                postData: formData
            }
            $("".loader"").show();
            promiseAjaxPost.call(addRequestJson, false).then(
                (res) => {
                    $("".loader"").hide();;
                    if (res.IsSuccess && res.Status == 200) {
                        messagePopup.responseSuccess(res.Message).then(
                            (r) => { if (r == true) { location.reload(); } }
                        )
                    } else if (res.IsSuccess && res.Status == 208) {
                        messagePopup.responseWarning(res.Message);
                    } else {
                        messagePopup.responseError(res.Message);
                    }
                },
                (err) => {
                    messagePopup.error(err.statusText);
                }
            )
            return false;

            });
        });
    </script>
");
            }
            );
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