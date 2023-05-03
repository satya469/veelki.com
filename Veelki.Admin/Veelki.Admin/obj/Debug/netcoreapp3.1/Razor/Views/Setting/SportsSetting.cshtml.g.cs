#pragma checksum "D:\Projects\veelki.com\Veelki.Admin\Veelki.Admin\Views\Setting\SportsSetting.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "8845aff65de2022b6b9ba48bbc690b71faa3f377"
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"8845aff65de2022b6b9ba48bbc690b71faa3f377", @"/Views/Setting/SportsSetting.cshtml")]
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
    Layout = "~/Views/Shared/_Layout.cshtml";

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
<div class=""container main_wrap"">
    <br />
    <br />
    <h1>Sports Settings</h1>
    <br />
    <br />
    <table id=""resultTable"" class=""table01 margin-table"" style=""display: table;"">
        <tbody>
            <tr>
                <th id=""accountTh"" width=""5%"" class=""align-L""");
            BeginWriteAttribute("style", " style=\"", 388, "\"", 396, 0);
            EndWriteAttribute();
            WriteLiteral(">\r\n                    S. No.\r\n                </th>\r\n                <th id=\"creditRefTh\" width=\"10%\"");
            BeginWriteAttribute("style", " style=\"", 499, "\"", 507, 0);
            EndWriteAttribute();
            WriteLiteral(">\r\n                    SPORT\r\n                </th>\r\n                <th id=\"initialCreditLimitTh\" width=\"10%\"");
            BeginWriteAttribute("style", " style=\"", 618, "\"", 626, 0);
            EndWriteAttribute();
            WriteLiteral(">\r\n                    ACTION\r\n                </th>\r\n                <th id=\"creditLimitTh\" width=\"9%\"");
            BeginWriteAttribute("style", " style=\"", 730, "\"", 738, 0);
            EndWriteAttribute();
            WriteLiteral(">\r\n                    HIGHLIGHT\r\n                </th>\r\n\r\n                <th id=\"todayPLTh\" width=\"9%\"");
            BeginWriteAttribute("style", " style=\"", 843, "\"", 851, 0);
            EndWriteAttribute();
            WriteLiteral(">\r\n                    SETTING\r\n                </th>\r\n            </tr>\r\n            <tr id=\"dataTemplate\" style=\"display: none\">\r\n                <td id=\"accountCol\"");
            BeginWriteAttribute("style", " style=\"", 1019, "\"", 1027, 0);
            EndWriteAttribute();
            WriteLiteral(" class=\"align-L\">\r\n                    <a id=\"account\" class=\"ico_account\" href=\"#\" style=\"float: none\"></a>\r\n                </td>\r\n                <td id=\"creditRef\" class=\"credit-amount-member\"");
            BeginWriteAttribute("style", " style=\"", 1224, "\"", 1232, 0);
            EndWriteAttribute();
            WriteLiteral(@">
                    <a id=""creditRefBtn"" class=""favor-set""
                       href=""javascript:void(0);""></a>
                </td>
                <td id=""initialCreditLimit"" style=""display: none""></td>
                <td id=""creditLimit"" style=""display: none""></td>

                <td id=""currentPL"" style=""display: none""></td>
                <td id=""transferablePL"" style=""display: none""></td>
                <td id=""balance""");
            BeginWriteAttribute("style", " style=\"", 1681, "\"", 1689, 0);
            EndWriteAttribute();
            WriteLiteral("></td>\r\n                <td id=\"currentExposure\"");
            BeginWriteAttribute("style", " style=\"", 1738, "\"", 1746, 0);
            EndWriteAttribute();
            WriteLiteral("></td>\r\n                <td id=\"available\"");
            BeginWriteAttribute("style", " style=\"", 1789, "\"", 1797, 0);
            EndWriteAttribute();
            WriteLiteral("></td>\r\n                <td id=\"playerBalance\" style=\"display: none\"></td>\r\n                <td id=\"exposureLimit\" style=\"display: none\"></td>\r\n\r\n                <td id=\"refPL\"");
            BeginWriteAttribute("style", " style=\"", 1974, "\"", 1982, 0);
            EndWriteAttribute();
            WriteLiteral("></td>\r\n                <td id=\"statusCol\"");
            BeginWriteAttribute("style", " style=\"", 2025, "\"", 2033, 0);
            EndWriteAttribute();
            WriteLiteral(">\r\n                    <ul id=\"tipsPopup\" class=\"status-popup\" style=\"display: none\">\r\n                    </ul>\r\n                    <span id=\"status\"></span>\r\n                </td>\r\n                <td id=\"actionCol\"");
            BeginWriteAttribute("style", " style=\"", 2252, "\"", 2260, 0);
            EndWriteAttribute();
            WriteLiteral(@">
                    <ul class=""action"">
                        <li>
                            <a id=""banking"" class=""bank"" href=""#bank"">
                                Bank
                            </a>
                        </li>
                        <li>
                            <a id=""p_l"" class=""p_l"" href=""#"">
                                Betting Profit &amp; Loss
                            </a>
                        </li>
                        <li>
                            <a id=""betting_history"" class=""betting_history"" href=""#"">
                                Betting History
                            </a>
                        </li>
                        <li>
                            <a id=""change"" class=""status"" href=""#change"">
                                Change Status
                            </a>
                        </li>
                        <li>
                            <a id=""profile"" class=""profile"" href=""#"">
         ");
            WriteLiteral("                       Profile\r\n                            </a>\r\n                        </li>\r\n                    </ul>\r\n                </td>\r\n            </tr>\r\n\r\n");
            WriteLiteral("\r\n");
            WriteLiteral("\r\n");
#nullable restore
#line 134 "D:\Projects\veelki.com\Veelki.Admin\Veelki.Admin\Views\Setting\SportsSetting.cshtml"
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
#line 140 "D:\Projects\veelki.com\Veelki.Admin\Veelki.Admin\Views\Setting\SportsSetting.cshtml"
                                   Write(i);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                        <td>");
#nullable restore
#line 141 "D:\Projects\veelki.com\Veelki.Admin\Veelki.Admin\Views\Setting\SportsSetting.cshtml"
                       Write(item.SportName);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                        <td>\r\n                            <a data-value=\"");
#nullable restore
#line 143 "D:\Projects\veelki.com\Veelki.Admin\Veelki.Admin\Views\Setting\SportsSetting.cshtml"
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
            BeginWriteAttribute("checked", " checked=\"", 5942, "\"", 5964, 1);
#nullable restore
#line 149 "D:\Projects\veelki.com\Veelki.Admin\Veelki.Admin\Views\Setting\SportsSetting.cshtml"
WriteAttributeValue("", 5952, item.Status, 5952, 12, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" name=\"select\"");
            BeginWriteAttribute("id", " id=\"", 5979, "\"", 5999, 2);
            WriteAttributeValue("", 5984, "Status_", 5984, 7, true);
#nullable restore
#line 149 "D:\Projects\veelki.com\Veelki.Admin\Veelki.Admin\Views\Setting\SportsSetting.cshtml"
WriteAttributeValue("", 5991, item.Id, 5991, 8, false);

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
#line 158 "D:\Projects\veelki.com\Veelki.Admin\Veelki.Admin\Views\Setting\SportsSetting.cshtml"
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
            BeginWriteAttribute("checked", " checked=\"", 6791, "\"", 6816, 1);
#nullable restore
#line 164 "D:\Projects\veelki.com\Veelki.Admin\Veelki.Admin\Views\Setting\SportsSetting.cshtml"
WriteAttributeValue("", 6801, item.Highlight, 6801, 15, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" name=\"select\"");
            BeginWriteAttribute("id", " id=\"", 6831, "\"", 6854, 2);
            WriteAttributeValue("", 6836, "Highlight_", 6836, 10, true);
#nullable restore
#line 164 "D:\Projects\veelki.com\Veelki.Admin\Veelki.Admin\Views\Setting\SportsSetting.cshtml"
WriteAttributeValue("", 6846, item.Id, 6846, 8, false);

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
                        <td style=""text-align:center;"">
                            <button");
            BeginWriteAttribute("id", " id=\"", 7276, "\"", 7302, 2);
            WriteAttributeValue("", 7281, "btnShowModal_", 7281, 13, true);
#nullable restore
#line 173 "D:\Projects\veelki.com\Veelki.Admin\Veelki.Admin\Views\Setting\SportsSetting.cshtml"
WriteAttributeValue("", 7294, item.Id, 7294, 8, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" type=\"button\"\r\n                                    class=\"btn btn-primary btn-sm showModel setting_btn\">\r\n                                Setting\r\n                            </button>\r\n\r\n                            <div");
            BeginWriteAttribute("id", " id=\"", 7524, "\"", 7550, 2);
            WriteAttributeValue("", 7529, "settingModal_", 7529, 13, true);
#nullable restore
#line 178 "D:\Projects\veelki.com\Veelki.Admin\Veelki.Admin\Views\Setting\SportsSetting.cshtml"
WriteAttributeValue("", 7542, item.Id, 7542, 8, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(@" style=""display: none;"" class=""modal"" data-backdrop=""static"" tabindex=""-1"" role=""dialog"" aria-labelledby=""staticBackdrop"" aria-hidden=""true"">
                                <div class=""modal-dialog"" role=""document"">
                                    <div class=""modal-content"">
                                        <div class=""pop_box"" style=""z-index:1;"">
                                            <a class=""close_pop hideModel"" href=""javascript: void(0)""");
            BeginWriteAttribute("id", " id=\"", 8018, "\"", 8044, 2);
            WriteAttributeValue("", 8023, "btnHideModal_", 8023, 13, true);
#nullable restore
#line 182 "D:\Projects\veelki.com\Veelki.Admin\Veelki.Admin\Views\Setting\SportsSetting.cshtml"
WriteAttributeValue("", 8036, item.Id, 8036, 8, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(@" data-dismiss=""modal"">close_pop</a>
                                            <h3>
                                                Sport Setting
                                            </h3>
                                            <div class=""modal-body"">
                                                <input type=""hidden""");
            BeginWriteAttribute("id", " id=\"", 8384, "\"", 8407, 2);
            WriteAttributeValue("", 8389, "sportName_", 8389, 10, true);
#nullable restore
#line 187 "D:\Projects\veelki.com\Veelki.Admin\Veelki.Admin\Views\Setting\SportsSetting.cshtml"
WriteAttributeValue("", 8399, item.Id, 8399, 8, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            BeginWriteAttribute("value", " value=\"", 8408, "\"", 8431, 1);
#nullable restore
#line 187 "D:\Projects\veelki.com\Veelki.Admin\Veelki.Admin\Views\Setting\SportsSetting.cshtml"
WriteAttributeValue("", 8416, item.SportName, 8416, 15, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" />\r\n                                                <input type=\"hidden\"");
            BeginWriteAttribute("id", " id=\"", 8505, "\"", 8527, 2);
            WriteAttributeValue("", 8510, "sequence_", 8510, 9, true);
#nullable restore
#line 188 "D:\Projects\veelki.com\Veelki.Admin\Veelki.Admin\Views\Setting\SportsSetting.cshtml"
WriteAttributeValue("", 8519, item.Id, 8519, 8, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            BeginWriteAttribute("value", " value=\"", 8528, "\"", 8550, 1);
#nullable restore
#line 188 "D:\Projects\veelki.com\Veelki.Admin\Veelki.Admin\Views\Setting\SportsSetting.cshtml"
WriteAttributeValue("", 8536, item.Sequence, 8536, 14, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(@" />
                                                <div class=""form-group"">
                                                    <label>Min Odd Limit</label>
                                                    <input class=""form-control"" type=""number"" placeholder=""Min Odd Limit...""");
            BeginWriteAttribute("id", " id=\"", 8836, "\"", 8861, 2);
            WriteAttributeValue("", 8841, "text_minOdd_", 8841, 12, true);
#nullable restore
#line 191 "D:\Projects\veelki.com\Veelki.Admin\Veelki.Admin\Views\Setting\SportsSetting.cshtml"
WriteAttributeValue("", 8853, item.Id, 8853, 8, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            BeginWriteAttribute("value", " value=\"", 8862, "\"", 8887, 1);
#nullable restore
#line 191 "D:\Projects\veelki.com\Veelki.Admin\Veelki.Admin\Views\Setting\SportsSetting.cshtml"
WriteAttributeValue("", 8870, item.MinOddLimit, 8870, 17, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(@" />
                                                </div>
                                                <div class=""form-group"">
                                                    <label>Max Odd Limit</label>
                                                    <input class=""form-control"" placeholder=""Max Odd Limit..."" type=""number""");
            BeginWriteAttribute("id", " id=\"", 9229, "\"", 9254, 2);
            WriteAttributeValue("", 9234, "text_maxOdd_", 9234, 12, true);
#nullable restore
#line 195 "D:\Projects\veelki.com\Veelki.Admin\Veelki.Admin\Views\Setting\SportsSetting.cshtml"
WriteAttributeValue("", 9246, item.Id, 9246, 8, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            BeginWriteAttribute("value", " value=\"", 9255, "\"", 9280, 1);
#nullable restore
#line 195 "D:\Projects\veelki.com\Veelki.Admin\Veelki.Admin\Views\Setting\SportsSetting.cshtml"
WriteAttributeValue("", 9263, item.MaxOddLimit, 9263, 17, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(@" />
                                                </div>
                                                <div class=""form-group"">
                                                    <label>Max Stack Limit</label>
                                                    <input class=""form-control"" type=""number"" placeholder=""Max Stack Limit...""");
            BeginWriteAttribute("id", " id=\"", 9626, "\"", 9658, 2);
            WriteAttributeValue("", 9631, "text_maxStackLimit_", 9631, 19, true);
#nullable restore
#line 199 "D:\Projects\veelki.com\Veelki.Admin\Veelki.Admin\Views\Setting\SportsSetting.cshtml"
WriteAttributeValue("", 9650, item.Id, 9650, 8, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            BeginWriteAttribute("value", " value=\"", 9659, "\"", 9686, 1);
#nullable restore
#line 199 "D:\Projects\veelki.com\Veelki.Admin\Veelki.Admin\Views\Setting\SportsSetting.cshtml"
WriteAttributeValue("", 9667, item.MaxStackLimit, 9667, 19, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(@" />
                                                </div>
                                                <div class=""form-group"">
                                                    <label>Bet Delay Time</label>
                                                    <input class=""form-control"" placeholder=""Bet Delay Time"" type=""number""");
            BeginWriteAttribute("id", " id=\"", 10027, "\"", 10058, 2);
            WriteAttributeValue("", 10032, "text_betDelayTime_", 10032, 18, true);
#nullable restore
#line 203 "D:\Projects\veelki.com\Veelki.Admin\Veelki.Admin\Views\Setting\SportsSetting.cshtml"
WriteAttributeValue("", 10050, item.Id, 10050, 8, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            BeginWriteAttribute("value", " value=\"", 10059, "\"", 10085, 1);
#nullable restore
#line 203 "D:\Projects\veelki.com\Veelki.Admin\Veelki.Admin\Views\Setting\SportsSetting.cshtml"
WriteAttributeValue("", 10067, item.BetDelayTime, 10067, 18, false);

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
            BeginWriteAttribute("checked", " checked=\"", 10533, "\"", 10555, 1);
#nullable restore
#line 209 "D:\Projects\veelki.com\Veelki.Admin\Veelki.Admin\Views\Setting\SportsSetting.cshtml"
WriteAttributeValue("", 10543, item.Status, 10543, 12, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" name=\"select\"");
            BeginWriteAttribute("id", " id=\"", 10570, "\"", 10598, 2);
            WriteAttributeValue("", 10575, "checkBoxStatus_", 10575, 15, true);
#nullable restore
#line 209 "D:\Projects\veelki.com\Veelki.Admin\Veelki.Admin\Views\Setting\SportsSetting.cshtml"
WriteAttributeValue("", 10590, item.Id, 10590, 8, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(@" />
                                                            <span></span>
                                                        </label>
                                                    </span>
                                                    <label>Highlight</label>
                                                    <span class=""switch switch-success"">
                                                        <label>
                                                            <input type=""checkbox""");
            BeginWriteAttribute("checked", " checked=\"", 11121, "\"", 11146, 1);
#nullable restore
#line 216 "D:\Projects\veelki.com\Veelki.Admin\Veelki.Admin\Views\Setting\SportsSetting.cshtml"
WriteAttributeValue("", 11131, item.Highlight, 11131, 15, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" name=\"select\"");
            BeginWriteAttribute("id", " id=\"", 11161, "\"", 11192, 2);
            WriteAttributeValue("", 11166, "checkBoxHighlight_", 11166, 18, true);
#nullable restore
#line 216 "D:\Projects\veelki.com\Veelki.Admin\Veelki.Admin\Views\Setting\SportsSetting.cshtml"
WriteAttributeValue("", 11184, item.Id, 11184, 8, false);

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
                                            <div class=""btn_box inline-form"">
                                                <div class=""btn_box"" style=""display: inline-flex;"">
                                                    <button");
            BeginWriteAttribute("id", " id=\"", 11747, "\"", 11770, 2);
            WriteAttributeValue("", 11752, "submitBtn_", 11752, 10, true);
#nullable restore
#line 224 "D:\Projects\veelki.com\Veelki.Admin\Veelki.Admin\Views\Setting\SportsSetting.cshtml"
WriteAttributeValue("", 11762, item.Id, 11762, 8, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" type=\"submit\" class=\"btn-send submitBtn\">Submit</button>\r\n                                                    <button type=\"button\"");
            BeginWriteAttribute("id", " id=\"", 11903, "\"", 11929, 2);
            WriteAttributeValue("", 11908, "btnHideModal_", 11908, 13, true);
#nullable restore
#line 225 "D:\Projects\veelki.com\Veelki.Admin\Veelki.Admin\Views\Setting\SportsSetting.cshtml"
WriteAttributeValue("", 11921, item.Id, 11921, 8, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(@" class=""btn-send hideModel"">close</button>
                                                </div>
                                            </div>

                                        </div>
                                    </div>
                                </div>
                            </div>
                        </td>
                    </tr>
");
#nullable restore
#line 235 "D:\Projects\veelki.com\Veelki.Admin\Veelki.Admin\Views\Setting\SportsSetting.cshtml"
                    i++;
                }
            }

#line default
#line hidden
#nullable disable
            WriteLiteral("        </tbody>\r\n    </table>\r\n");
            WriteLiteral("</div>\r\n");
            DefineSection("PageJs", async() => {
                WriteLiteral(@"
    <script type=""text/javascript"">
        $(document).ready(function () {
            $("".showModel"").click(function () {
                var id = $(this).attr(""id"").split(""_"")[1];
                //$(""#settingModal_"" + id).removeClass(""fade"");
                var modal = document.getElementById(""settingModal_"" + id);
                modal.style.display = ""block"";
                //$(""#settingModal_"" + id).modal('show');
            });

            $("".hideModel"").click(function () {
                var id = $(this).attr(""id"").split(""_"")[1];
                //$(""#settingModal_"" + id).addClass(""fade"");
                //$(""#settingModal_"" + id).modal('hide');
                var modal = document.getElementById(""settingModal_"" + id);
                modal.style.display = ""none"";
            });

            $("".submitBtn"").click(function () {
                debugger;
                //alert($(this).attr(""id"").split(""_"")[1]);
                const id = $(this).attr(""id"").split(""_"")[1];");
                WriteLiteral(@"

                const minOddLimit = $(""#text_minOdd_"" + id).val();
                const maxOddLimit = $(""#text_maxOdd_"" + id).val();
                const sportName = $(""#sportName_"" + id).val();
                const sequence = $(""#sequence_"" + id).val();
                const status = document.getElementById(""checkBoxStatus_"" + id).checked;
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
              ");
                WriteLiteral("  formData.append(\"Sequence\", sequence);\r\n\r\n                let addRequestJson = {\r\n                apiUrl: \"");
#nullable restore
#line 406 "D:\Projects\veelki.com\Veelki.Admin\Veelki.Admin\Views\Setting\SportsSetting.cshtml"
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
