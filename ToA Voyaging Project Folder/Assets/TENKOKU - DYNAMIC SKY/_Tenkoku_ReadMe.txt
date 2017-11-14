---------------------------------------------------
TENKOKU - DYNAMIC SKY

Copyright ©2017 Tanuki Digital
Version 1.1.6
---------------------------------------------------


----------------------------
THANK YOU FOR YOUR PURCHASE!
----------------------------
Thank you for buying TENKOKU and supporting Tanuki Digital!
It's people like you that allow us to build and improve our software! 
if you have any questions, comments, or requests for new features
please visit the Tanuki Digital Forums and post your feedback:

http://tanukidigital.com/forum/

or email us directly at: konnichiwa@tanukidigital.com



----------------------
REGISTER YOUR PURCHASE
----------------------
Did you purchase Tenkoku - Dynamic Sky on the Unity Asset Store?
Registering at Tanuki Digital.com gives you immediate access to new downloads, updates, and exclusive content as well as Tenkoku and Tanuki Digital news and info.  Fill out the registration forum using your Asset Store "OR" Order Number here:

http://www.tanukidigital.com/tenkoku/index.php?register=1



----------------------
SUPPORT
----------------------
If you have questions about Tenkoku, need help with a feature, or think you've identified a bug please let us know either in the Unity forum or on the Tanuki Digital forum below.

Unity Forum Thread: http://forum.unity3d.com/threads/tenkoku-dynamic-sky.318166/
Tanuki Digital Forum: http://tanukidigital.com/forum/

You can also email us directly at: konnichiwa@tanukidigital.com



----------------------
DOCUMENTATION
----------------------
Please read the Tenkoku documentation files for more in-depth customization information.
http://tanukidigital.com/tenkoku/documentation



-------------
INSTALLATION
-------------
I. IMPORT TENKOKU FILES INTO YOUR PROJCT
Go to: “Assets -> Import Package -> Custom Package...” in the Unity Menu and select the “tenkoku_dynamicsky_ver1.x.unitypackage” file. This will open an import dialog box. Click the import button and all the Tenkoku files will be imported into your project list.

II. ADD THE TENKOKU MODULE TO YOUR SCENE
1) Drag the Tenkoku DynamicSky prefab located in the “/PREFABS” folder into your scene list.
2) If it isn’t set already, make sure to set the Tenkoku DynamicSky’s position in the transform settings to 0,0,0

III. ADD TENKOKU EFFECTS TO YOUR CAMERA
1) Click on your main camera object and add the Tenkoku Fog effect by going to Component-->Image Effects-->Tenkoku-->Tenkoku Fog.
Note: For best results this effect should be placed to render BEFORE your Tonemapping effect(if applicable).

(optional)
2) Click on your main camera object and add the Tenkoku Sun Shaft effect by going to Component-->Image Effects-->Tenkoku-->Tenkoku Sun Shafts.
Note: For best results this effect should be placed to render AFTER your Tonemapping effect(if applicable).


A Note About Scene Cameras:
Tenkoku relies on tracking your main scene camera in order to properly update in the scene.  By default Tenkoku attempts to auto locate your camera by selecting the camera in your scene with the ‘MainCamera’ tag.  Alternatively you can set it to manual mode and drag the appropriate camera into the ‘Scene Camera’ slot.




-------------
NOTES
-------------
A Note On Accuracy:
Moon and planet position calculations are currently accurate for years ranging between 1900ca - 2100ca.  The further away from the year 2000ca that you get (in either direction) the more noticeable calculation errors will become.  Additional calculation methods are currently being looked at to increase the accuracy range for these objects.

Integration with CTS - Complete Terrain Shader:
CTS is an advanced terrain shader for Unity with built-in settings to control wetness, rain, snow, and seasonal tinting directly on the terrain itself.  Tenkoku provides an integration script which will automatically drive the effects in CTS according to the weather settings you use in Tenkoku.  To enable this integration, install CTS and Tenkoku in your project, add the CTS weather Manager, thenf inally drag the /SCRIPTS/Tenkoku_CTS_Weather component onto the 'CTS Weather Manager' object in your scene.
Learn more about CTS here:  https://www.assetstore.unity3d.com/en/#!/content/91938




-------------------------------
RELEASE NOTES - Version 1.1.6
-------------------------------

WHAT'S NEW
- Added Integration component with CTS - Complete Terrain Shader.
- Added night light Fill for better night visibility.  Control with Sky Amount (Night) setting.
- Added 'Temperature Tint' gradient that tints scene colors based on weather temperature setting.
- Added public boolean flag variable (isDoingTransition) to test for transitions from code.
- Added Wind Speed slider to Random Weather Generator tab.
- Added 'Fog Obscurance' property under Atmospherics to handle sky/fog distance blending.
- Added 'Auto Adjust Fog' setting under Atmospherics.  Enabled by default.
- Added Weather Humidity setting.  Automatically adjusts fog distance and rendering when enabled.
- Added subtle moon lighting to overcast clouds at night.
- Added Sun Flare setting under 'Atmospherics' section.
- Added Moon Horizon Tint option, to manually control low-horizon moon color.
- Added 'Use Full Screen Aliasing' option.  Off by default.  Useful when showing reflections of clouds in your scene.
- Added 'Adjust Overcast Ambient' setting to lighten ambient shadows during overcast weather.

CHANGES
- Adjusted default sky lighting to be darker (previous settings were confusing to those not using Tonemapping.)
- Adjusted Dusk/Dawn lighting to be less red.
- Changed rain/snow/fog particle collisions to 'default' Unity layer.  Should help with default performance.
- Random Weather generator no longer overrides wind speed.
- Removed forced moon shadows.  Set to soft shadows by default, but LIGHT_NightSky object can now be edited.
- Adjusted cloud transparency and lighting slightly.
- Added additional tooltips.
- Reflection probe intensity no longer forced to 1.0, but is set at 1.0 by default.
- Removed Fog Dispersion setting.  No longer applicable.

BUG FIXES
- Fixed sky calculation timing error when set to large time multipliers (3600+).
- Fixed Scattering calculation shader which was preventing proper color rendering in DX9 and MacOSX.
- Fixed sky shader errors when used in PS4 render target.
- Fixed new sky atmosphere shader to work properly on MacOSX.
- Fixed fog/sky tracking error while switching scene cameras.
- Fixed overdarkening of Stratus and Cirrus clouds.
- Fixed bug showing incongruous horizon line when during overcast weather.
- Fixed issue with y-reversal in fog buffer when using Forward and MSAA.
- Fixed intermittent issue with Aurora render clipping.
- Fixed distant dark horizon line and lower atmosphere rendering fill.
- Fixed 'Fade Distance' setting, now blends out distant fog properly again.
- Fixed camera .hdr switch error in Unity 5.6+
- Fixed error with Scene Light Layer list population while playing scene.
- Fixed outline rendering issues in cumulus and stratus clouds.
- Fixed auroa compositing with Legacy Clouds.
- Fixed blank screen error when Light rays are enabled and temporal aliasing is disabled.
- Fixed prolonged delay with initial reflection probe and ambient update.


----------------------------
CREDITS
----------------------------
- Cloud System adapted from prototype work by Keijiro Takahashi.  Used with permission.
https://github.com/keijiro

- Temporal Reprojection is modified from Playdead Games' public implementation, available here:
https://github.com/playdeadgames/temporal

- Oskar Elek Atmospheric model originally developed in 2009 by Oskar Elek.  Model has been adapted for Unity by Michal Skalsky (2016) as part of his Public Domain work regarding volumetric atmospheric rendering:
http://www.oskee.wz.cz/stranka/oskee.php
https://github.com/SlightlyMad

- Lunar image adapted from texture work by James Hastings-Trew.  Used with permission.
http://planetpixelemporium.com

- Galaxy image adapted from an open source image made available by the European Southern Observatory(ESO/S. Brunier):
https://www.eso.org/public/usa/images/eso0932a/

- Star position and magnitude data is taken from the Yale Bright Star Catalog, 5th Edition:
http://tdc-www.harvard.edu/catalogs/bsc5.html (archive)
http://heasarc.gsfc.nasa.gov/W3Browse/star-catalog/bsc5p.html (data overview)

- Calculation algorithms for Sun, Moon, and Planet positions have been adapted from work published by Paul Schlyter, in his paper 'Computing Planetary Positions'.  I've taken liberties with his procedure where I thought appropriate or where I found it best suits the greater Tenkoku system.  You can read his original paper here:
http://www.stjarnhimlen.se/comp/ppcomp.html  
