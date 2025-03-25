![image](https://github.com/user-attachments/assets/86368f8a-1031-4805-bef0-a25399ac402f)


<p><center><b>Code Generator for FullStackHero .NET 9 Starter Kit</b></center></p>

<p><center><b>This Code generator for FullStackHero .NET9 Starter Kit enables you to quickly generate all 25 API files needed for CRUD endpoint creation directly from your model.</b></center></p>

<b>Features:</b>
<ul>
<li>Add route & service configuration to catalogmodule.cs</li>
<li>Root namespace can be changed individually</li>
<li>Plural entity name can be set</li>
<li>Outputs directly in your Visual Studio project hierarchy or into a temporary folder</li>
<li>Windows GUI with many features, selectable model, property and build options</li>
</ul>

<b>Prerequisites:</b>
<ul>
<li>Microsoft .NET 9</li>
<li>DevExpress Winforms 24.2</li>
<li>Installed FullStackHero dotnet-starter-kit with original Folder Structure</li>
</ul>
<b>Quick Start:</b>
<ul>
<li>Clone Git repository</li>
<li>Set all files in template directory to build action "none" (maybe git clone do this already for you)</li>
<li>Write your model and entity configuration, remark the “QueueDomainEvent” call, since the model has recursive depencies with the API (QueueDomainEvent) files</li>
<li>Update Entity-framework context</li>
<li>Build your project (Codegen searches for the model in the compiled assembly)</li>
<li>Enjoy code generation and save a lot of time!</li>
</ul>
