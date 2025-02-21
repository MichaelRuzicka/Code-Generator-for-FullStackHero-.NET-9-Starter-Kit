using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;

namespace CodeGen
{
    public class Codebuilder : ICodebuilder
    {
        BuilderParams _BuilderParams;
        private readonly IEnumerable<ITemplateLineHandler> _LineHandlers;
        public BuilderParams Params { get => _BuilderParams; set => _BuilderParams = value; }

        public Codebuilder(IEnumerable<ITemplateLineHandler> lineHandlers)
        {
            _LineHandlers = lineHandlers;
        }


        public Task<bool> BuildAsync()
        {
            if (Params == null)
            {
                throw new ArgumentNullException(nameof(Params));
            }

            foreach (string templateFilename in Params.TemplatePaths)
            {
                TemplateLineReader(templateFilename); //Read Tenplate
            }

            return Task.FromResult(true);
        }

        private void TemplateLineReader(string templateFilename)
        {
            List<string> newLines = new List<string>();
            using (StreamReader templateFile = new StreamReader(templateFilename))
            {

                int counter = 0;
                string line;

                while ((line = templateFile.ReadLine()) != null)
                {
                    Console.WriteLine(line);
                    ProcessTemplateLine(ref line);
                    newLines.Add(line);
                    Console.WriteLine(line);
                    counter++;
                }
                templateFile.Close();
                Console.WriteLine($"{counter} lines processed.");
            }
            TemplateLineWriter(newLines.ToArray(),Path.GetFileName(templateFilename)
                .Replace("Template",Params.Entity));
        }



        private void ProcessTemplateLine(ref string templateLine)
        {
            foreach (var lineHandler in _LineHandlers)
            {
                lineHandler.DoTemplating(Params, ref templateLine);
            }
        }




        private void TemplateLineWriter(string[] newlines, string fileName)
        {
            string folder = Params.OutputPath;

            string fullPath = folder + @"\" + Path.GetFileName(fileName);
            File.WriteAllLines(fullPath, newlines);
        }

    }
}
//[ModuleName] zb.Catalog
//[Module_Namespace]   Default: FSH.Starter.WebApi.Catalog
//[EntitySet] Default: Entity Plural zb.Brands
//[Entity]
//[Entity_]
//[Entity_PropertyId]
//[DataType]
//[PropertyName] Default: zb.Description
//[DefaultValue]