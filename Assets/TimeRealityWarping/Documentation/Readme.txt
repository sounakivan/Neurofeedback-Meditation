Time / Reality Warping Effects includes two image effects and eight prefabs you can drag into your scene.

Image Effects:

	Screen Ripple: 
	Image effect that produces a distortion ring from a point in screen space.
		To Use: Drag the ScreenRipple.cs script onto your camera. Parameters can be changed through the editor, scripts, or animation.
		Parameters:
			Ripple Origin -
				- World Space: Check this to have the ripple's point of origin be calculated from a point in world space.
				Otherwise, it will take a position in screen space.
				- Origin Type: When World Space is checked, choose whether to input a position manually or have the origin point
				follow an object's Transform.
				- Origin Position: The position, in either screen space of world space, of the origin point.
				- Origin Object: The Transform that the origin point will follow. Can only be used if World Space is checked.
			Properties - 
				- Ring Size: How far the ripple is expanded from the origin point. A value of 0 disables the effect.
				- Falloff Distance: Distance from the origin point when the ripple fades completely.
				- Distortion: How much the ripple distorts the image.
				- Ring Thickness: How thick the ripple is in screen space.
				- Fade Amount - The amount the original image is blended with the distorted image. A value of 1 will not show any distortion.
			Color - 
				- Color: A color that can optionally be added or subtracted from the image based on the amount of distortion.
				- Color Blend Type:
					- None: No color is used.
					- Additive: The color is added to the image based on the amount of distortion.
					- Subtractive: The color is subtracted from the image based on the amount of distortion.
			Normal Map - 
				- Normal Map: Optional normal map texture added to the distortion.
				- Normal Scale: The normal map's tiling scale.
				- Normal Offset Scroll: Normal map scrolling speed.
				- Bump Strength: How much the normal texture affects distortion.

	Screen Ripple Continuous: 
	Image effect that produces continuous distortion rings that expand from a point in screen space over time.
		To Use: Drag the ScreenRippleContinuous.cs script onto your camera. Parameters can be changed through the editor, scripts, or animation.
		Parameters:
			Ripple Origin -
				- World Space: Check this to have the ripple's point of origin be calculated from a point in world space.
				Otherwise, it will take a position in screen space.
				- Origin Type: When World Space is checked, choose whether to input a position manually or have the origin point
				follow an object's Transform.
				- Origin Position: The position, in either screen space of world space, of the origin point.
				- Origin Object: The Transform that the origin point will follow. Can only be used if World Space is checked.
			Properties - 
				- Effect Radius: Radius of the effect in screen space. A value of 0 turns off the effect.
				- Distortion: How much the ripples distort the image.
				- Frequency: The greater the value, the more waves between the origin and the effect radius at once.
				- Speed: The speed at which the ripples expand outward. A negative value will move the ripples toward the origin.
				- Ring Thinness: How thin each wave is. The greater the value, the thinner and farther apart each wave. Keep value at 2 for even spacing and thinness.
				- Fade Amount - The amount the original image is blended with the distorted image. A value of 1 will not show any distortion.
			Color - 
				- Color: A color that can optionally be added or subtracted from the image based on the amount of distortion.
				- Color Blend Type:
					- None: No color is used.
					- Additive: The color is added to the image based on the amount of distortion.
					- Subtractive: The color is subtracted from the image based on the amount of distortion.
			Normal Map - 
				- Normal Map: Optional normal map texture added to the distortion.
				- Normal Scale: The normal map's tiling scale.
				- Normal Offset Scroll: Normal map scrolling speed.
				- Bump Strength: How much the normal texture affects distortion.

Object Effects:

	Ripple Object: 
	Produces a distortion ring from a point on a mesh. A similar effect to Screen Ripple, but using a mesh in the world.
	To Use: Drag the Ripple Plane prefab into your scene. Parameters can be changed through the editor, scripts, or animation.
		To use the effect on a different mesh, assign the ObjectRipple_MAT material to the mesh and attach the ObjectRipple.cs script.

		Parameters:
			- Billboard: When checked, the object will orient itself to the camera.
			- Cam: The camera the object will orient to. If nothing is assigned, it will default to the main camera.

			- Origin Offset: Offset the origin point from texture center (-0.5 - 0.5)
			- Distortion: How much the ripple distorts the image.
			- Falloff Distance: Distance from the origin point when the ripple fades completely.
			- Ring Size: How far the ripple is expanded from the origin point. A value of 0 disables the effect.	
			- Ring Thickness: How thick the ripple is in screen space.
			- Fade Amount - The amount the original image is blended with the distorted image. A value of 1 will not show any distortion.

			Color - 
				- Color: A color that can optionally be added or subtracted from the image based on the amount of distortion.
				- Color Blend Type:
					- None: No color is used.
					- Additive: The color is added to the image based on the amount of distortion.
					- Subtractive: The color is subtracted from the image based on the amount of distortion.
			Normal Map - 
				- Normal Map: Optional normal map texture added to the distortion.
				- Normal Scale: The normal map's tiling scale.
				- Normal Offset Scroll: Normal map scrolling speed.
				- Bump Strength: How much the normal texture affects distortion.

	Ripple Object Continuous: 
	Produces a continuous distortion rings from a point on a mesh. A similar effect to Screen Ripple Continuous, but using a mesh in the world.
	To Use: Drag the Ripple Continuous Plane prefab into your scene. Parameters can be changed through the editor, scripts, or animation.
		To use the effect on a different mesh, assign the ObjectRippleContinuous_MAT material to the mesh and attach the ObjectRippleContinuous.cs script.

		Parameters:
			- Billboard: When checked, the object will orient itself to the camera.
			- Cam: The camera the object will orient to. If nothing is assigned, it will default to the main camera.

			- Origin Offset: Offset the origin point from texture center (-0.5 - 0.5)
			- Effect Radius: Radius of the effect. A value of 0 turns off the effect.
			- Distortion: How much the ripples distort the image.
			- Frequency: The greater the value, the more waves between the origin and the effect radius at once.
			- Speed: The speed at which the ripples expand outward. A negative value will move the ripples toward the origin.
			- Ring Thinness: How thin each wave is. The greater the value, the thinner and farther apart each wave. Keep value at 2 for even spacing and thinness.	
			- Fade Amount - The amount the original image is blended with the distorted image. A value of 1 will not show any distortion.

			Color - 
				- Color: A color that can optionally be added or subtracted from the image based on the amount of distortion.
				- Color Blend Type:
					- None: No color is used.
					- Additive: The color is added to the image based on the amount of distortion.
					- Subtractive: The color is subtracted from the image based on the amount of distortion.
			Normal Map - 
				- Normal Map: Optional normal map texture added to the distortion.
				- Normal Scale: The normal map's tiling scale.
				- Normal Offset Scroll: Normal map scrolling speed.
				- Bump Strength: How much the normal texture affects distortion.

	Distortion Hit:
	A quick hit effect with a burst of sparks that distorts whatever is behind it. After the single burst, the object destroys itself.
	To Use: Instantiate through script.
	Parameters:
		- Destroy After Burst: Uncheck to keep the object in the scene after it bursts.

	Energy Ball:
	Particle effect with billboarded distortion waves behind it.
	To Use: Drag into your scene or instantiate through script.

	Portal:
	Portal particle effect using normal map distortion.
	To Use: Drag into your scene or instantiate through script.

	Dark Portal:
	Variation of previous portal effect with diffrent colors and normal map distortion.
	To Use: Drag into your scene or instantiate through script.

	Time Burst:
	Burst effect that appears to distort the whole environment.
	To Use: Drag object into your scene or attach the TimeBurst.cs script onto an existing object.
	Assign camera with Screen Ripple component attached. Adjust parameters in editor or through script.
	Activate effect by calling Burst().

	Pulse Ring:
	Continuous pulse effect that repeatedly expands a ripple outward.
	To Use: Drag object into your scene or attach the PulseRing.cs script onto an existing object.
	Assign camera with Screen Ripple component attached. Adjust parameters in editor or through script.
	Effect will play as long as it's active.

	For any questions, feel free to contact me at MikBraverman@gmail.com.