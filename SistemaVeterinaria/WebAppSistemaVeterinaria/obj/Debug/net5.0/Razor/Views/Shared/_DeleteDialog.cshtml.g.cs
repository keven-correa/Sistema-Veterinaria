#pragma checksum "C:\Users\Keven\OneDrive\Escritorio\ProyectoIII vet\SistemaVeterinaria\WebAppSistemaVeterinaria\Views\Shared\_DeleteDialog.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "57e5158424badfcc27245ee493f2c06fe6f2f8b4"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Shared__DeleteDialog), @"mvc.1.0.view", @"/Views/Shared/_DeleteDialog.cshtml")]
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
#line 1 "C:\Users\Keven\OneDrive\Escritorio\ProyectoIII vet\SistemaVeterinaria\WebAppSistemaVeterinaria\Views\_ViewImports.cshtml"
using WebAppSistemaVeterinaria;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\Keven\OneDrive\Escritorio\ProyectoIII vet\SistemaVeterinaria\WebAppSistemaVeterinaria\Views\_ViewImports.cshtml"
using WebAppSistemaVeterinaria.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"57e5158424badfcc27245ee493f2c06fe6f2f8b4", @"/Views/Shared/_DeleteDialog.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"916084ebd457e3c4a152df2dcbffe494a2d26684", @"/Views/_ViewImports.cshtml")]
    public class Views_Shared__DeleteDialog : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral(@"<!--Delete Item-->
<div class=""modal fade"" id=""deleteDialog"" tabindex=""-1"" role=""dialog"" aria-labelledby=""exampleModalLabel"" aria-hidden=""true"">
    <div class=""modal-dialog"" role=""document"">
        <div class=""modal-content"">
            <div class=""modal-header"">
                <h5 class=""modal-title"" id=""exampleModalLabel"">Borrar Registro</h5>
                <button type=""button"" class=""close"" data-dismiss=""modal"" aria-label=""Close"">
                    <span aria-hidden=""true"">&times;</span>
                </button>
            </div>
            <div class=""modal-body"">
                <p>Deseas eliminar el registro?</p>
            </div>
            <div class=""modal-footer"">
                <button type=""button"" class=""btn btn-primary"" data-dismiss=""modal"">Cerrar</button>
                <button type=""button"" class=""btn btn-danger"" id=""btnYesDelete"">Borrar</button>
            </div>
        </div>
    </div>
</div>
");
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
