using DevExpress.Mvvm.POCO;
using DevExpress.XtraEditors;
using Microsoft.Win32;
using System;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using System.Collections.Generic;
using DevExpress.XtraEditors.Controls;
using System.IO;
using Microsoft.Extensions.DependencyInjection;
using DevExpress.XtraTreeList;
using DevExpress.CodeParser;

namespace CodeGen
{
    public partial class frmMain : XtraForm
    {
        Assembly modelAssembly;
        public frmMain()
        {
            InitializeComponent();

            SetFolderDefaults();

        }

        private void btnFrameWork_Click(object sender, EventArgs e)
        {
            xtraOpenFileDialog1.InitialDirectory = System.IO.Path.GetDirectoryName(txtFileNameDomain.Text);
            xtraOpenFileDialog1.Filter = "dll files (*.dll)|*.dll|All files (*.*)|*.*";
            if (xtraOpenFileDialog1.ShowDialog() == DialogResult.OK)
            {
                txtFileNameDomain.Text = xtraOpenFileDialog1.FileName;
                var subkey = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\MiruLogic\CodeGen");
                subkey.SetValue("FrameWorkPath", txtFileNameDomain.Text);
            }
        }

        private void btnLoadDomain_Click(object sender, EventArgs e)
        {
            xtraOpenFileDialog1.InitialDirectory = System.IO.Path.GetDirectoryName(txtFileNameDomain.Text);
            xtraOpenFileDialog1.Filter = "dll files (*.dll)|*.dll|All files (*.*)|*.*";
            if (xtraOpenFileDialog1.ShowDialog() == DialogResult.OK)
            {
                txtFileNameDomain.Text = xtraOpenFileDialog1.FileName;
                var subkey = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\MiruLogic\CodeGen");
                subkey.SetValue("ModelPath", txtFileNameDomain.Text);
            }
        }

        private void btnOutputPath_Click(object sender, EventArgs e)
        {
            XtraFolderBrowserDialog xtraFolderBrowserDialog = new XtraFolderBrowserDialog();
            xtraFolderBrowserDialog.SelectedPath = txtOutputPath.Text;
            xtraFolderBrowserDialog.KeepPosition = true;
            xtraFolderBrowserDialog.ShowDragDropConfirmation = true;
            xtraFolderBrowserDialog.StartPosition = FormStartPosition.CenterParent;
            xtraFolderBrowserDialog.ShowDialog();
            txtOutputPath.Text = xtraFolderBrowserDialog.SelectedPath;
            var subkey = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\MiruLogic\CodeGen");
            subkey.SetValue("OutputPath", txtOutputPath.Text);
        }

        private void btnProjectApiPath_Click(object sender, EventArgs e)
        {
            XtraFolderBrowserDialog xtraFolderBrowserDialog = new XtraFolderBrowserDialog();
            xtraFolderBrowserDialog.SelectedPath = txtProjectApiPath.Text;
            xtraFolderBrowserDialog.KeepPosition = true;
            xtraFolderBrowserDialog.ShowDragDropConfirmation = true;
            xtraFolderBrowserDialog.StartPosition = FormStartPosition.CenterParent;
            xtraFolderBrowserDialog.ShowDialog();
            txtProjectApiPath.Text = xtraFolderBrowserDialog.SelectedPath;
            var subkey = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\MiruLogic\CodeGen");
            subkey.SetValue("ProjectApiPath", txtProjectApiPath.Text);
        }
        private void btnParse_Click(object sender, EventArgs e)
        {

            var loadedAssemblies = AppDomain.CurrentDomain.GetAssemblies().ToList();


            modelAssembly = Assembly.LoadFrom(txtFileNameDomain.Text);
            var referencedAssemblys = modelAssembly.GetReferencedAssemblies();

            foreach (AssemblyName aName in referencedAssemblys)
            {
                if (aName.Name == "FSH.Framework.Core")
                    loadedAssemblies.Add(Assembly.LoadFrom(txtFileNameCore.Text));
                else

                    loadedAssemblies.Add(AppDomain.CurrentDomain.Load(aName.FullName));

            }
            cbEntity.Properties.Items.AddRange(modelAssembly.DefinedTypes.Where(dt => dt.ImplementedInterfaces.Any(ii => ii.Name.Contains("IEntity"))).ToList());
            cbEntity.Enabled = true;
            txtEntitynamePlural.Enabled = true;
            grpPO.Enabled = true;
            btnBuildOutputDir.Enabled = true;
            btnBuildProject.Enabled = true;
        }


        private void cbEntity_SelectedValueChanged(object sender, EventArgs e)
        {
            checkedListBoxControl1.Items.Clear();
            var entityType = (Type)cbEntity.EditValue;
            var entity = Activator.CreateInstance(entityType);
            var properties = entityType.GetProperties(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.DeclaredOnly);

            checkedListBoxControl1.Items.AddRange(properties);
            ((BaseCheckedListBoxControl)checkedListBoxControl1).CheckAll();
            txtEntitynamePlural.Text = entityType.Name + "s";
        }

        private void chkSelectAllInfra_CheckedChanged(object sender, EventArgs e)
        {
            chkEndpoints.Checked = chkSelectAllInfra.Checked;
            chkRoutesRegister.Checked = chkSelectAllInfra.Checked;
        }

        private void chkSelectAllDomain_CheckedChanged(object sender, EventArgs e)
        {
            chkDomainEvents.Checked = chkSelectAllDomain.Checked;
            chkDomainExceptions.Checked = chkSelectAllDomain.Checked;
        }

        private void chkSelectAllApp_CheckedChanged(object sender, EventArgs e)
        {
            chkMediatRCreate.Checked = chkSelectAllApp.Checked;
            chkMediatRDelete.Checked = chkSelectAllApp.Checked;
            chkMediatREvents.Checked = chkSelectAllApp.Checked;
            chkMediatRGet.Checked = chkSelectAllApp.Checked;
            chkMediatRSearch.Checked = chkSelectAllApp.Checked;
            chkMediatRUpdate.Checked = chkSelectAllApp.Checked;
        }

        private void btnBuildOutputDir_Click(object sender, EventArgs e)
        {
            TemplateSelector(OutputEnum.OutputDir);
        }

        private void btnBuildProject_Click(object sender, EventArgs e)
        {
            TemplateSelector(OutputEnum.ProjectDir);
        }

        private void TemplateSelector(OutputEnum outputDestination)
        {
            List<PropertyInfo> propertyInfos = new List<PropertyInfo>();
            foreach (CheckedListBoxItem item in checkedListBoxControl1.CheckedItems)
                propertyInfos.Add(item.Value as PropertyInfo);


            BuilderParams builderParams = new BuilderParams
            {
                OutputDestination = outputDestination,
                ModuleName = txtModulName.Text,
                Root_Namespace = txtRootNameSpace.Text,
                Module_Namespace = txtModulNamepace.Text,
                EntitySet = txtEntitynamePlural.Text,
                Entity = ((Type)cbEntity.SelectedItem).Name,
                PropertyInfos = propertyInfos
            };

            if (outputDestination == OutputEnum.OutputDir)
                builderParams.OutputPath = txtOutputPath.Text;
            else
                builderParams.OutputPath = txtProjectApiPath.Text;


            if (chkEndpoints.Checked)
            {
                ProcessTemplate(builderParams, "Endpoints");
            }

            if (chkRoutesRegister.Checked)
            {
                ProcessTemplate(builderParams, "RoutesAndRegisterServices");
            }

            if (chkDomainEvents.Checked)
            {
                ProcessTemplate(builderParams, "DomainEvents");
            }


            if (chkDomainExceptions.Checked)
            {
                ProcessTemplate(builderParams, "DomainExceptions");
            }

            if (chkMediatRCreate.Checked)
            {
                ProcessTemplate(builderParams, "MediatRCreate");
            }


            if (chkMediatRDelete.Checked)
            {
                ProcessTemplate(builderParams, "MediatRDelete");
            }


            if (chkMediatREvents.Checked)
            {
                ProcessTemplate(builderParams, "MediatREvents");
            }

            if (chkMediatRGet.Checked)
            {
                ProcessTemplate(builderParams, "MediatRGet");
            }


            if (chkMediatRSearch.Checked)
            {
                ProcessTemplate(builderParams, "MediatRSearch");
            }

            if (chkMediatRUpdate.Checked)
            {
                ProcessTemplate(builderParams, "MediatRUpdate");
            }
        }

        private void ProcessTemplate(BuilderParams builderParams, string templateName)
        {
            string TemplateRootDirectory = GetTemplateDirectory();
            string WorkingTemplateDirectory = string.Empty;

            Codebuilder codebuilder = Program.ServiceProvider.GetRequiredService<Codebuilder>() as Codebuilder;

            switch (templateName)
            {
                case "MediatRCreate":
                    WorkingTemplateDirectory = TemplateRootDirectory + @"\Domain\Application\Create\v1\";
                    builderParams.TemplateName = templateName;
                    builderParams.TemplatePaths.Clear();
                    builderParams.TemplatePaths.Add(WorkingTemplateDirectory + "CreateTemplateCommand.cs");
                    builderParams.TemplatePaths.Add(WorkingTemplateDirectory + "CreateTemplateCommandValidator.cs");
                    builderParams.TemplatePaths.Add(WorkingTemplateDirectory + "CreateTemplateHandler.cs");
                    builderParams.TemplatePaths.Add(WorkingTemplateDirectory + "CreateTemplateResponse.cs");
                    codebuilder.Params = builderParams;
                    codebuilder.Build();
                    break;


                case "MediatRDelete":
                    WorkingTemplateDirectory = TemplateRootDirectory + @"\Domain\Application\Delete\v1\";
                    builderParams.TemplateName = templateName;
                    builderParams.TemplatePaths.Clear();
                    builderParams.TemplatePaths.Add(WorkingTemplateDirectory + "DeleteTemplateCommand.cs");
                    builderParams.TemplatePaths.Add(WorkingTemplateDirectory + "DeleteTemplateHandler.cs");
                    codebuilder.Params = builderParams;
                    codebuilder.Build();
                    break;


                case "MediatREvents":
                    WorkingTemplateDirectory = TemplateRootDirectory + @"\Domain\Application\EventHandlers\";
                    builderParams.TemplateName = templateName;
                    builderParams.TemplatePaths.Clear();
                    builderParams.TemplatePaths.Add(WorkingTemplateDirectory + "TemplateCreatedEventHandler.cs");
                    codebuilder.Params = builderParams;
                    codebuilder.Build();
                    break;

                case "MediatRGet":
                    WorkingTemplateDirectory = TemplateRootDirectory + @"\Domain\Application\Get\v1\";
                    builderParams.TemplateName = templateName;
                    builderParams.TemplatePaths.Clear();
                    builderParams.TemplatePaths.Add(WorkingTemplateDirectory + "GetTemplateHandler.cs");
                    builderParams.TemplatePaths.Add(WorkingTemplateDirectory + "GetTemplateRequest.cs");
                    builderParams.TemplatePaths.Add(WorkingTemplateDirectory + "TemplateResponse.cs");
                    codebuilder.Params = builderParams;
                    codebuilder.Build();
                    break;

                case "MediatRSearch":
                    WorkingTemplateDirectory = TemplateRootDirectory + @"\Domain\Application\Search\v1\";
                    builderParams.TemplateName = templateName;
                    builderParams.TemplatePaths.Clear();
                    builderParams.TemplatePaths.Add(WorkingTemplateDirectory + "SearchTemplateCommand.cs");
                    builderParams.TemplatePaths.Add(WorkingTemplateDirectory + "SearchTemplateHandler.cs");
                    builderParams.TemplatePaths.Add(WorkingTemplateDirectory + "SearchTemplateSpecs.cs");
                    codebuilder.Params = builderParams;
                    codebuilder.Build();
                    break;

                case "MediatRUpdate":
                    WorkingTemplateDirectory = TemplateRootDirectory + @"\Domain\Application\Update\v1\";
                    builderParams.TemplateName = templateName;
                    builderParams.TemplatePaths.Clear();
                    builderParams.TemplatePaths.Add(WorkingTemplateDirectory + "UpdateTemplateCommand.cs");
                    builderParams.TemplatePaths.Add(WorkingTemplateDirectory + "UpdateTemplateCommandValidator.cs");
                    builderParams.TemplatePaths.Add(WorkingTemplateDirectory + "UpdateTemplateHandler.cs");
                    builderParams.TemplatePaths.Add(WorkingTemplateDirectory + "UpdateTemplateResponse.cs");
                    codebuilder.Params = builderParams;
                    codebuilder.Build();
                    break;

                case "DomainEvents":
                    WorkingTemplateDirectory = TemplateRootDirectory + @"\Domain\Events\";
                    builderParams.TemplateName = templateName;
                    builderParams.TemplatePaths.Clear();
                    builderParams.TemplatePaths.Add(WorkingTemplateDirectory + "TemplateCreated.cs");
                    builderParams.TemplatePaths.Add(WorkingTemplateDirectory + "TemplateUpdated.cs");
                    codebuilder.Params = builderParams;
                    codebuilder.Build();
                    break;

                case "DomainExceptions":
                    WorkingTemplateDirectory = TemplateRootDirectory + @"\Domain\Exceptions\";
                    builderParams.TemplateName = templateName;
                    builderParams.TemplatePaths.Clear();
                    builderParams.TemplatePaths.Add(WorkingTemplateDirectory + "TemplateNotFoundException.cs");
                    codebuilder.Params = builderParams;
                    codebuilder.Build();
                    break;

                case "Endpoints":
                    WorkingTemplateDirectory = TemplateRootDirectory + @"\Infrastructure\Endpoints\v1\";
                    builderParams.TemplateName = templateName;
                    builderParams.TemplatePaths.Clear();
                    builderParams.TemplatePaths.Add(WorkingTemplateDirectory + "CreateTemplateEndpoint.cs");
                    builderParams.TemplatePaths.Add(WorkingTemplateDirectory + "DeleteTemplateEndpoint.cs");
                    builderParams.TemplatePaths.Add(WorkingTemplateDirectory + "GetTemplateEndpoint.cs");
                    builderParams.TemplatePaths.Add(WorkingTemplateDirectory + "SearchTemplateEndpoint.cs");
                    builderParams.TemplatePaths.Add(WorkingTemplateDirectory + "UpdateTemplateEndpoint.cs");
                    codebuilder.Params = builderParams;
                    codebuilder.Build();
                    break;

                case "RoutesAndRegisterServices":
                    if (builderParams.OutputDestination == OutputEnum.ProjectDir) //In Project Output Mode the RoutesRegister Template is read & modified from (real) Solution not Template Directory
                        WorkingTemplateDirectory = $"{builderParams.OutputPath}\\Modules\\{builderParams.ModuleName}\\{builderParams.ModuleName}.Infrastructure\\";
                    else
                        WorkingTemplateDirectory = TemplateRootDirectory + @"\Infrastructure\";
                    builderParams.TemplateName = templateName;
                    builderParams.TemplatePaths.Clear();

                    if (builderParams.OutputDestination == OutputEnum.ProjectDir)
                        builderParams.TemplatePaths.Add(WorkingTemplateDirectory + $"{builderParams.ModuleName}Module.cs");
                    else
                        builderParams.TemplatePaths.Add(WorkingTemplateDirectory + "TemplateModule.cs");
                    codebuilder.Params = builderParams;
                    codebuilder.Build();
                    break;



                default:
                    break;
            }
        }




        private static string GetTemplateDirectory()
        {
            return Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + @"\Templates";
        }



        private void SetFolderDefaults()
        {
            string defaultDomainPath = @"C:\Users\Radioactive\Source\repos\dotnet-starter-kit\src\api\modules\Catalog\Catalog.Domain\bin\Debug\net9.0";
            RegistryKey domainPath = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\MiruLogic\CodeGen");
            if (domainPath == null)
            {
                txtFileNameDomain.Text = defaultDomainPath;
            }
            else
            {
                txtFileNameDomain.Text = domainPath.GetValue("ModelPath", defaultDomainPath)?.ToString();
            }


            string defaultFrameworkPath = @"C:\Users\Radioactive\Source\repos\dotnet-starter-kit\src\api\modules\Catalog\Catalog.Domain\bin\Debug\net9.0";
            RegistryKey frameWorkPath = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\MiruLogic\CodeGen");
            if (frameWorkPath == null)
            {
                txtFileNameCore.Text = defaultFrameworkPath;
            }
            else
            {
                txtFileNameCore.Text = frameWorkPath.GetValue("FrameWorkPath", defaultFrameworkPath)?.ToString();
            }


            string defaultOutputPath = GetTemplateDirectory();
            RegistryKey outputPath = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\MiruLogic\CodeGen");
            if (outputPath == null)
            {
                txtOutputPath.Text = defaultOutputPath;
            }
            else
            {
                txtOutputPath.Text = outputPath.GetValue("OutputPath", defaultOutputPath)?.ToString();
            }


            RegistryKey projectApiPath = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\MiruLogic\CodeGen");
            if (projectApiPath != null)
                txtProjectApiPath.Text = projectApiPath.GetValue("ProjectApiPath", defaultOutputPath)?.ToString();
        }


    }
}

