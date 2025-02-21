<p><center><b>Code Generator FSH dotnet-starter-kit VS Template</b></center></p>

<b>Features:</b>
<ul>
<li>Generates all 25 API files needed for CRUD / Endpoint creation from your selected model type with a few clicks</li>
<li>Add Route & Service Configuration to Catalogmodule.cs</li>
<li>Root Namespace can be changed</li>
<li>Plural Entity name can be set</li>
<li>Writes directly in your VS project hierachy or in a tempory folder selected</li>
<li>Nice Gui with many features, you can select Model / Propertys & Build Options</li>
</ul>
<b>How to install:</b>
<ul>
<li>Clone repository</li>
<li>Set all Files in Template Directory to Build Action "none" (may git clone do this already for you)</li>
<li>Write your model & Entity Configuration, remark the QueueDomainEvent call since the model has recursive depencies with the api (QueueDomainEvent) files</li>
<li>Update Ef Context  </li>
<li>Build your project (Codegen searches for the model in the compiled assembly)  </li>
<li>Rest is self explaining</li>
</ul>
<b>Requirements:</b>
<ul>
<li>.Net9</li>
<li>DevEx Winforms 24.2</li>
<li>Installed FSH dotnet-starter-kit with original Folder Structure</li>
</ul>




