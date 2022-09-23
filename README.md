Note: Regarding the DBContext in the device controller please refer to the Data Access Layer section below<br />
The code of the Web App was provided by the lecturer due to the project focusing on Standards & Patterns, 
the code that was changed by the student to successfully complete this project is in the controller classes, 
Startup.cs and apppsetinggs.json(that is hidden in .gitignore). Further, the student created a folder named 
Repository to implement a tier 2 repository pattern, meaning all my controller repositories inherit from one 
interface that fulfils all the SOLID principles. The student created very custom methods for the generic 
repository which is worth checking out, the custom methods minimize the amount of code that needs to be 
repeated and maximise reusability(with considering the dependency inversion principle). Nevertheless, a 
section will still be discussed on how to use the Web App: <br />
<h2>How to use the Web App</h2>
<p>
	In order to use the Web App of student 34494847 the user needs to click this link: <a href="https://webapp-34494847.azurewebsites.net/">WebApp-34494847</a>.
	Once the web page has landed, the user will be greeted with the following banner: <br />
	<img src="img/banner.png" alt="API-34494847 landing banner image"/><br />
	The user then needs to specify the specific controller in the URL to use its endpoints.
	For example, if the user wants to use the category controller, then the URL would be specified like this which is the same for devices and zones: <br />
	<img src="img/categoryurl.png" alt="API-34494847 login dropdown image"/><br />
	After specifying the specific controller in the URL that wants to be used, the user will see a page like this which is also similar for devices and zones: <br />
	<img src="img/categorieslanding.png" alt="API-34494847 login dropdown image"/><br />
	The user can then choose to edit, view or delete an existing record or create a new record by clicking these icons: <br />
	Edit: <br />
	<img src="img/edit.png" alt="API-34494847 login dropdown image"/><br />
	Which then gives this page: <br />
	<img src="img/editC.png" alt="API-34494847 login dropdown image"/><br />
	View: <br />
	<img src="img/view.png" alt="API-34494847 login dropdown image"/><br />
	Which then gives this page: <br />
	<img src="img/viewC.png" alt="API-34494847 login dropdown image"/><br />
	Delete: <br />
	<img src="img/delete.png" alt="API-34494847 login dropdown image"/><br />
	Which then gives this page: <br />
	<img src="img/deleteC.png" alt="API-34494847 login dropdown image"/><br />
	Create: <br />
	<img src="img/create.png" alt="API-34494847 login dropdown image"/><br />
	Which then gives this page: <br />
	<img src="img/createC.png" alt="API-34494847 login dropdown image"/><br />
	And that's it, the user can access any of these endpoints for categories, devices and zones in the Web App of student 34494847
</p>
<h2>Data Access Layer</h2>
<p>
	The ConnectedOfficeContext is only referenced in the GenericRepository class, except at the DevicesController about which I 
	consulted Prof Marijke Coetzee on 2022/09/20 in class and we established that I must use the ConnectedOfficeContext in the 
	DevicesController for the ViewData render (dropdown on the webpage) and she said I will not be penalized for this. The 
	student used the .json file that was provided by the lecturer which means that the student used a connection string to an 
	online database that the lecturer is hosting. Lastly, all SOLID principles were successfully applied as discussed previously, 
	meaning that data was not accessed directly from each controller.
</p>
<h2>Repository Layer</h2>
<p>
	The student has successfully implemented all classes and interfaces: <br />
	<img src="img/repository.png" alt="API-34494847 login dropdown image"/><br />
	For which dependency injection was done in the Startup.cs file: <br />
	<img src="img/dependency.png" alt="API-34494847 login dropdown image"/><br />
</p>
<h2>Standards</h2>
<p>
	All SOLID principles were successfully applied as discussed previously. No code was repeated due to the student creating custom 
	methods in the generic repository which was also discussed previously. Both the single responsibility principle and the open-closed 
	principle were applied due to successfully applying SOLID. The student provided short but very descriptive commented annotations 
	at each method in each controller and at some classes to explain the inheritance(where does the class come from). The student 
	removed any non-essential commented-out code and as said previously only provided short but very descriptive commented annotations 
	to essentially provide clean code and not bombard the classes with commentary. The student applied the dependency inversion principle 
	which means that changes in one class should not force other classes to change, so the whole application can become maintainable and 
	extensible. This was done by carefully designing custom methods in the generic repository as discussed previously.
</p>
