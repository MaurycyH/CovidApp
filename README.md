# CovidApp
The application was created in Xamarin.Forms in order to train my skills in C #. 
I wrote the entire application myself. I tried to keep the MVVM pattern throughout the entire application structure.
It uses the local SQLite database and the [Xamarin.Forms.GoogleMaps](https://github.com/amay077/Xamarin.Forms.GoogleMaps) nuget package

## Technologies
Project is created with:
* Xamarin.Forms
* SQLite Database
* Xamarin.Forms.GoogleMaps
* [Rg.Plugins.Popup](https://github.com/rotorgames/Rg.Plugins.Popup)

## Setup
To run this project, you can just download the project, and run it locally but you need your own google API_key in Android Manifest for Google Maps to work otherwise you will see a grey map.


```
		<meta-data android:name="com.google.android.geo.API_KEY" android:value="TOKEN" />
```
## Screenshots
Below I am pasting sample screenshots from the application. The interface is in Polish.
<p float="left">
  <img src="https://user-images.githubusercontent.com/72604629/115298066-406f5280-a15d-11eb-9326-6d8566678610.png" width="40%" height="50%" />
  <img src="https://user-images.githubusercontent.com/72604629/115298102-50873200-a15d-11eb-87fe-83f161878887.png" width="40%" height="50%"/> 
</p>
<p float="left">
  <img src="https://user-images.githubusercontent.com/72604629/115298114-541ab900-a15d-11eb-93cc-fa48db0b8cc5.png" width="40%" height="50%" />
  <img src="https://user-images.githubusercontent.com/72604629/115298121-567d1300-a15d-11eb-995c-94e04e9d013e.png" width="40%" height="50%"/> 
</p>
<p float="left">
  <img src="https://user-images.githubusercontent.com/72604629/115298129-5aa93080-a15d-11eb-976a-a5eb0fe6e674.png" width="40%" height="50%" />
  <img src="https://user-images.githubusercontent.com/72604629/115298130-5b41c700-a15d-11eb-8211-8f396a28f3db.png" width="40%" height="50%"/> 
</p>

