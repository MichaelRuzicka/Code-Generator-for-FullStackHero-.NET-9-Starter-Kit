using DevExpress.CodeParser;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.Design;

namespace CodeGen
{
    public class Codebuilder
    {
        BuilderParams _BuilderParams;
        private readonly IEnumerable<ITemplateLineHandler> _LineHandlers;
        public BuilderParams Params { get => _BuilderParams; set => _BuilderParams = value; }

        public Codebuilder(IEnumerable<ITemplateLineHandler> lineHandlers)
        {
            _LineHandlers = lineHandlers;
        }


        public void Build()
        {
            if (Params == null)
            {
                throw new ArgumentNullException(nameof(Params));
            }

            foreach (string templateFilename in Params.TemplatePaths)
            {
                string fileName = Path.GetFileName(templateFilename);
                if (!Params.EntityIsIAggregateRoot) //Skip CRUD Templates for non Aggregate Roots
                {
                    if (fileName.EndsWith(@"Module.cs")) //PosModule Registration for Endpoints & Services
                    {
                        TemplateLineWriterProjectDir(Path.GetFileName(templateFilename).Replace("Template", Params.Entity));
                        continue;
                    }
                    if (fileName.StartsWith(@"Create") && fileName != "CreateTemplateCommandValidator.cs") //Validators should stay
                    {
                        TemplateLineWriterProjectDir(Path.GetFileName(templateFilename).Replace("Template", Params.Entity));
                        continue;
                    }
                    if (fileName.StartsWith(@"Update") && fileName != "UpdateTemplateCommandValidator.cs")//Validators should stay
                    {
                        TemplateLineWriterProjectDir(Path.GetFileName(templateFilename).Replace("Template", Params.Entity));
                        continue;
                    }
                    if (fileName.StartsWith(@"Delete"))
                    {
                        TemplateLineWriterProjectDir(Path.GetFileName(templateFilename).Replace("Template", Params.Entity));
                        continue;
                    }
                    if (fileName.StartsWith(@"Get"))
                    {
                        TemplateLineWriterProjectDir(Path.GetFileName(templateFilename).Replace("Template", Params.Entity));
                        continue;
                    }
                    if (fileName.StartsWith(@"Search"))
                    {
                        TemplateLineWriterProjectDir(Path.GetFileName(templateFilename).Replace("Template", Params.Entity));
                        continue;
                    }
                }

                TemplateLineReader(templateFilename); //Read Template
            }
        }
  
        private void TemplateLineReader(string templateFilename)
        {
            bool RoutesAndRegisterServicesExists = false;
            List<string> newLines = new List<string>();
            using (StreamReader templateFile = new StreamReader(templateFilename))
            {
                int counter = 0;
                string line;

                while ((line = templateFile.ReadLine()) != null)
                {
                    Console.WriteLine(line);
                    if (Params.TemplateName == "RoutesAndRegisterServices")
                    {
                        if (line.Contains($"MapGroup(\"{Params.EntitySet.ToLower()}\")"))
                        {
                            Console.WriteLine("Skip Templating Service Routes / Register  Section, already exists");
                            RoutesAndRegisterServicesExists = true;
                            break;
                        }
                    }
                    if (line.Contains("[") && line.Contains("]"))
                        ProcessTemplateLine(ref line);
                    newLines.Add(line);
                    Console.WriteLine(line);
                    counter++;
                }
                templateFile.Close();
                Console.WriteLine($"{counter} lines processed.");
            }

            if (RoutesAndRegisterServicesExists)
            {
                Console.WriteLine("Skip Writing to file, Service Routes / Register  Section, already exists");
                return;
            }

            if (Params.TemplateName == "RoutesAndRegisterServices")
            {
                if (Params.OutputDestination == OutputEnum.OutputDir)
                    TemplateLineWriterOutputDir(newLines.ToArray(), Path.GetFileName(templateFilename).Replace("Template", Params.ModuleName));
                else
                    TemplateLineWriterProjectDir(newLines.ToArray(), Path.GetFileName(templateFilename).Replace("Template", Params.ModuleName), true);
            }
            else
            {

                switch (Path.GetFileName(templateFilename))//Change Validator Base Class to Domain Entity
                {
                    case "CreateTemplateCommandValidator.cs":
                        newLines = newLines.Select(s => s.Replace($"AbstractValidator<Create{Params.Entity}Command>", $"AbstractValidator<{Params.Root_Namespace}.WebApi.Pos.Domain.{Params.Entity}>")).ToList();
                        break;
                    case "UpdateTemplateCommandValidator.cs":
                        newLines = newLines.Select(s => s.Replace($"AbstractValidator<Update{Params.Entity}Command>", $"AbstractValidator<{Params.Root_Namespace}.WebApi.Pos.Domain.{Params.Entity}>")).ToList();
                        break;
                }

                if (Params.OutputDestination == OutputEnum.OutputDir)
                    TemplateLineWriterOutputDir(newLines.ToArray(), Path.GetFileName(templateFilename).Replace("Template", Params.Entity));
                else
                    TemplateLineWriterProjectDir(newLines.ToArray(), Path.GetFileName(templateFilename).Replace("Template", Params.Entity),true);
            }
        }


        private void ProcessTemplateLine(ref string templateLine)
        {
            foreach (var lineHandler in _LineHandlers)
            {
                lineHandler.DoTemplating(Params, ref templateLine);
            }
        }

        private void TemplateLineWriterOutputDir(string[] newlines, string templateFileNamePath)
        {
            string folder = Params.OutputPath;

            string fullPath = folder + @"\" + Path.GetFileName(templateFileNamePath);
            File.WriteAllLines(fullPath, newlines);
        }

        private void TemplateLineWriterProjectDir(string fileName)
        {
            TemplateLineWriterProjectDir(null, fileName, false);
        }

        private void TemplateLineWriterProjectDir(string[] newlines, string fileName, bool writeAction)
        {
            string folder = Params.OutputPath;
            string templateFileName = string.Empty;

            if (Params.TemplateName == "RoutesAndRegisterServices")
                templateFileName = Path.GetFileName(fileName).Replace(Params.ModuleName, "Template"); //Rename Back since switch/case cant be dynamic
            else
                templateFileName = Path.GetFileName(fileName).Replace(Params.Entity, "Template"); //Rename Back since switch/case cant be dynamic
            string projectPath = string.Empty;
            switch (templateFileName)
            {
                #region Application

                //SolutionPath: WebApi/Modules/[Module]/[Module].Application/[EntitySet]/

                //FilePath: api\modules\[Module]\[Module].Application\[EntitySet]\

                //Create/v1/
                case "CreateTemplateCommand.cs":
                case "CreateTemplateCommandValidator.cs":
                case "CreateTemplateHandler.cs":
                case "CreateTemplateResponse.cs":
                    projectPath = $"{Params.OutputPath}\\Modules\\{Params.ModuleName}\\{Params.ModuleName}.Application\\{Params.EntitySet}\\Create\\v1\\";
                    if (writeAction)
                    {
                        Directory.CreateDirectory(Path.GetFullPath(projectPath));
                        File.WriteAllLines($"{projectPath}{fileName}", newlines);
                    }
                    else
                        File.Delete($"{projectPath}{fileName}");
                    break;

                //Delete/v1/
                case "DeleteTemplateCommand.cs":
                case "DeleteTemplateHandler.cs":
                    projectPath = $"{Params.OutputPath}\\Modules\\{Params.ModuleName}\\{Params.ModuleName}.Application\\{Params.EntitySet}\\Delete\\v1\\";
                    if (writeAction)
                    {
                        Directory.CreateDirectory(Path.GetFullPath(projectPath));
                        File.WriteAllLines($"{projectPath}{fileName}", newlines);
                    }
                    else
                        File.Delete($"{projectPath}{fileName}");
                    break;

                //EventHandlers/v1/
                case "TemplateCreatedEventHandler.cs":
                    projectPath = $"{Params.OutputPath}\\Modules\\{Params.ModuleName}\\{Params.ModuleName}.Application\\{Params.EntitySet}\\EventHandlers\\v1\\";
                    Directory.CreateDirectory(Path.GetFullPath(projectPath));
                    File.WriteAllLines($"{projectPath}{fileName}", newlines);
                    break;

                //Get/v1/
                case "GetTemplateHandler.cs":
                case "GetTemplateRequest.cs":
                case "GetTemplateSpecs.cs":
                case "TemplateResponse.cs":

                    projectPath = $"{Params.OutputPath}\\Modules\\{Params.ModuleName}\\{Params.ModuleName}.Application\\{Params.EntitySet}\\Get\\v1\\";
                    if (writeAction)
                    {
                        Directory.CreateDirectory(Path.GetFullPath(projectPath));
                        File.WriteAllLines($"{projectPath}{fileName}", newlines);
                    }
                    else
                        File.Delete($"{projectPath}{fileName}");
                    break;

                //Search/v1/
                case "SearchTemplateCommand.cs":
                case "SearchTemplateHandler.cs":
                case "SearchTemplateSpecs.cs":
                    projectPath = $"{Params.OutputPath}\\Modules\\{Params.ModuleName}\\{Params.ModuleName}.Application\\{Params.EntitySet}\\Search\\v1\\";
                    if (writeAction)
                    {
                        Directory.CreateDirectory(Path.GetFullPath(projectPath));
                        File.WriteAllLines($"{projectPath}{fileName}", newlines);
                    }
                    else
                        File.Delete($"{projectPath}{fileName}");
                    break;

                //Update/v1/
                case "UpdateTemplateCommand.cs":
                case "UpdateTemplateCommandValidator.cs":
                case "UpdateTemplateHandler.cs":
                case "UpdateTemplateResponse.cs":

                    projectPath = $"{Params.OutputPath}\\Modules\\{Params.ModuleName}\\{Params.ModuleName}.Application\\{Params.EntitySet}\\Update\\v1\\";
                    if (writeAction)
                    {
                        Directory.CreateDirectory(Path.GetFullPath(projectPath));
                        File.WriteAllLines($"{projectPath}{fileName}", newlines);
                    }
                    else
                        File.Delete($"{projectPath}{fileName}");
                    break;
                #endregion


                #region Domain
                //Domain/Events/
                case "TemplateCreated.cs":
                case "TemplateUpdated.cs":
                    projectPath = $"{Params.OutputPath}\\Modules\\{Params.ModuleName}\\{Params.ModuleName}.Domain\\Events\\";
                    Directory.CreateDirectory(Path.GetFullPath(projectPath));
                    File.WriteAllLines($"{projectPath}{fileName}", newlines);
                    break;

                //Domain/Exceptions/
                case "TemplateNotFoundException.cs":
                    projectPath = $"{Params.OutputPath}\\Modules\\{Params.ModuleName}\\{Params.ModuleName}.Domain\\Exceptions\\";
                    Directory.CreateDirectory(Path.GetFullPath(projectPath));
                    File.WriteAllLines($"{projectPath}{fileName}", newlines);
                    break;
                #endregion

                //Infrastructure/Endpoints/v1/
                case "CreateTemplateEndpoint.cs":
                case "DeleteTemplateEndpoint.cs":
                case "GetTemplateEndpoint.cs":
                case "SearchTemplateEndpoint.cs":
                case "UpdateTemplateEndpoint.cs":
                    if (writeAction)
                    {
                        projectPath = $"{Params.OutputPath}\\Modules\\{Params.ModuleName}\\{Params.ModuleName}.Infrastructure\\Endpoints\\v1\\";
                        Directory.CreateDirectory(Path.GetFullPath(projectPath));
                        File.WriteAllLines($"{projectPath}{fileName}", newlines);
                    }
                    else
                        File.Delete($"{projectPath}{fileName}");
                    break;

                //Infrastructure/
                case "TemplateModule.cs":
                    if (writeAction)
                    {
                        projectPath = $"{Params.OutputPath}\\Modules\\{Params.ModuleName}\\{Params.ModuleName}.Infrastructure\\";
                        Directory.CreateDirectory(Path.GetFullPath(projectPath));
                        File.WriteAllLines($"{projectPath}{fileName}", newlines);
                    }
                    break;


                default:
                    break;
            }



        }


    }
}
