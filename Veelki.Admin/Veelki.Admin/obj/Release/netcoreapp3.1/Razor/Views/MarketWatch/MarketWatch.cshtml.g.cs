#pragma checksum "D:\Projects\veelki.com\Veelki.Admin\Veelki.Admin\Views\MarketWatch\MarketWatch.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "20690f81a8b0d2f6aa908edafc8a274db1fbd47c"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_MarketWatch_MarketWatch), @"mvc.1.0.view", @"/Views/MarketWatch/MarketWatch.cshtml")]
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
#nullable restore
#line 2 "D:\Projects\veelki.com\Veelki.Admin\Veelki.Admin\Views\MarketWatch\MarketWatch.cshtml"
using Veelki.Data.Entities;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"20690f81a8b0d2f6aa908edafc8a274db1fbd47c", @"/Views/MarketWatch/MarketWatch.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"79c6561a5a268df3ed5470800eadc04f69a403d9", @"/Views/_ViewImports.cshtml")]
    public class Views_MarketWatch_MarketWatch : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<Veelki.Data.Entities.Matches>>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("value", "0", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("name", "_matchList", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/js/custom.js"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        #pragma warning restore 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __backed__tagHelperScopeManager = null;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __tagHelperScopeManager
        {
            get
            {
                if (__backed__tagHelperScopeManager == null)
                {
                    __backed__tagHelperScopeManager = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager(StartTagHelperWritingScope, EndTagHelperWritingScope);
                }
                return __backed__tagHelperScopeManager;
            }
        }
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.OptionTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.PartialTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper;
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 3 "D:\Projects\veelki.com\Veelki.Admin\Veelki.Admin\Views\MarketWatch\MarketWatch.cshtml"
  
    ViewData["Title"] = "MarketWatch";
    Layout = "~/Views/Shared/_AdminLayout.cshtml"; var sportsList = ViewBag.SportsList as List<Sports>;

#line default
#line hidden
#nullable disable
            WriteLiteral(@"<div class=""container"">
    <h2>Market Watch</h2>
    <div class=""row p-4 form-group"">
        <div class=""col-md-3"">
            <label class=""control-label"">Sport Name</label>

            <select class=""form-control"" id=""ddlSportName"" onchange=""getSeriesName()"">
                ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "20690f81a8b0d2f6aa908edafc8a274db1fbd47c5193", async() => {
                WriteLiteral("Please Select Sport");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.OptionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n");
#nullable restore
#line 15 "D:\Projects\veelki.com\Veelki.Admin\Veelki.Admin\Views\MarketWatch\MarketWatch.cshtml"
                 foreach (var item in sportsList)
                {

#line default
#line hidden
#nullable disable
            WriteLiteral("                    ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "20690f81a8b0d2f6aa908edafc8a274db1fbd47c6653", async() => {
#nullable restore
#line 17 "D:\Projects\veelki.com\Veelki.Admin\Veelki.Admin\Views\MarketWatch\MarketWatch.cshtml"
                                        Write(item.SportName);

#line default
#line hidden
#nullable disable
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.OptionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
            BeginWriteTagHelperAttribute();
#nullable restore
#line 17 "D:\Projects\veelki.com\Veelki.Admin\Veelki.Admin\Views\MarketWatch\MarketWatch.cshtml"
                       WriteLiteral(item.Id);

#line default
#line hidden
#nullable disable
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("value", __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n");
#nullable restore
#line 18 "D:\Projects\veelki.com\Veelki.Admin\Veelki.Admin\Views\MarketWatch\MarketWatch.cshtml"
                }

#line default
#line hidden
#nullable disable
            WriteLiteral(@"            </select>
        </div>
        <div class=""col-md-3"">
            <label class=""control-label"">Event Name</label>
            <select class=""form-control"" id=""ddlSeries"">
            </select>
        </div>

        <div class=""col-md-3 mt-auto"">
            <button class=""btn btn-primary"" type=""button"" onclick=""Filter()"">Filter</button>
        </div>
    </div>
    <div id=""filterList"">
        ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("partial", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "20690f81a8b0d2f6aa908edafc8a274db1fbd47c9121", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.PartialTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper.Name = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
#nullable restore
#line 32 "D:\Projects\veelki.com\Veelki.Admin\Veelki.Admin\Views\MarketWatch\MarketWatch.cshtml"
__Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper.Model = Model;

#line default
#line hidden
#nullable disable
            __tagHelperExecutionContext.AddTagHelperAttribute("model", __Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper.Model, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n    </div>\r\n</div>\r\n\r\n");
            DefineSection("PageJs", async() => {
                WriteLiteral("\r\n    ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "20690f81a8b0d2f6aa908edafc8a274db1fbd47c10819", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_2);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral("\r\n    <script type=\"text/javascript\">\r\n\r\n        function getSeriesName() {\r\n            const formData = new FormData();\r\n            formData.append(\"SportId\", $(\'#ddlSportName\').val());\r\n\r\n            let getEventJson = {\r\n                apiUrl: \"");
#nullable restore
#line 45 "D:\Projects\veelki.com\Veelki.Admin\Veelki.Admin\Views\MarketWatch\MarketWatch.cshtml"
                    Write(Url.Action("GetBetEventData", "marketwatch"));

#line default
#line hidden
#nullable disable
                WriteLiteral(@""",
                postData: formData
            }
            $("".loader"").show();
            promiseAjaxPost.call(getEventJson, false).then(
                (res) => {
                    $("".loader"").hide();;
                    $('#ddlSeries').find('option').remove().end();
                    if (res.length>0) {
                        $.each(res, function (key, value) {
                            $('#ddlSeries').append($(""<option></option>"").val(value.tournamentId).html(value.tournamentName));
                        });
                    }
                    else {

                        $('#ddlSeries').append($(""<option></option>"").val(0).html('No Event'));
                    }
                },
                (err) => {
                    messagePopup.error(err.statusText);
                }
            )
        }

        function Filter() {
            const formData = new FormData();
            formData.append(""SportId"", $('#ddlSportName').val());
         ");
                WriteLiteral("   formData.append(\"SeriesId\", $(\'#ddlSeries\').val());\r\n            let getAircraftJson = {\r\n                apiUrl: \"");
#nullable restore
#line 74 "D:\Projects\veelki.com\Veelki.Admin\Veelki.Admin\Views\MarketWatch\MarketWatch.cshtml"
                    Write(Url.Action("GetSeriesMatchList", "marketwatch"));

#line default
#line hidden
#nullable disable
                WriteLiteral(@""",
                postData: formData,
                dataType: 'html'
            }
            $("".loader-1.loading-img"").show();
            promiseAjaxPost.call(getAircraftJson, false).then(
                (res) => {
                    $("".loader-1.loading-img"").hide();
                    $(""#filterList"").html(res);
                },
                (err) => {
                    messagePopup.error(err.statusText);
                }
            )
        }
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<Veelki.Data.Entities.Matches>> Html { get; private set; }
    }
}
#pragma warning restore 1591
