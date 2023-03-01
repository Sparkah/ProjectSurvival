/* Written by Kaz Crowe */
/* UltimateJoystickWindow.cs */
using UnityEngine;
using UnityEditor;
using UnityEditor.AnimatedValues;
using System.Collections.Generic;

public class UltimateJoystickWindow : EditorWindow
{
	static string version = "2.5.1";// ALWAYS UDPATE
	static int importantChanges = 2;// UPDATE ON IMPORTANT CHANGES
	// 2 > 2.5
	// 1 > ?
	static string menuTitle = "Main Menu";

	// LAYOUT STYLES //
	int sectionSpace = 20;
	int itemHeaderSpace = 10;
	int paragraphSpace = 5;
	GUIStyle sectionHeaderStyle = new GUIStyle();
	GUIStyle itemHeaderStyle = new GUIStyle();
	GUIStyle paragraphStyle = new GUIStyle();

	GUILayoutOption[] buttonSize = new GUILayoutOption[] { GUILayout.Width( 200 ), GUILayout.Height( 35 ) }; 
	GUILayoutOption[] docSize = new GUILayoutOption[] { GUILayout.Width( 300 ), GUILayout.Height( 330 ) };
	GUISkin style;
	Texture2D scriptReference;
	Texture2D ubPromo, usbPromo;

	class PageInformation
	{
		public string pageName = "";
		public Vector2 scrollPosition = Vector2.zero;
		public delegate void TargetMethod();
		public TargetMethod targetMethod;
	}
	static PageInformation mainMenu = new PageInformation() { pageName = "Main Menu" };
	static PageInformation howTo = new PageInformation() { pageName = "How To" };
	static PageInformation overview = new PageInformation() { pageName = "Overview" };
	static PageInformation documentation = new PageInformation() { pageName = "Documentation" };
	static PageInformation extras = new PageInformation() { pageName = "Extras" };
	static PageInformation otherProducts = new PageInformation() { pageName = "Other Products" };
	static PageInformation feedback = new PageInformation() { pageName = "Feedback" };
	static PageInformation changeLog = new PageInformation() { pageName = "Change Log" };
	static PageInformation versionChanges = new PageInformation() { pageName = "Version Changes" };
	static PageInformation thankYou = new PageInformation() { pageName = "Thank You" };
	static PageInformation settings = new PageInformation() { pageName = "Window Settings" };
	static List<PageInformation> pageHistory = new List<PageInformation>();
	static PageInformation currentPage = new PageInformation();

	enum FontSize
	{
		Small,
		Medium,
		Large
	}
	FontSize fontSize = FontSize.Small;
	bool configuredFontSize = false;

	#region Documentation Info
	class DocumentationInfo
	{
		public string functionName = "";
		public AnimBool showMore = new AnimBool( false );
		public string[] parameter;
		public string returnType = "";
		public string description = "";
		public string codeExample = "";
	}
	DocumentationInfo p_UpdatePositioning = new DocumentationInfo()
	{
		functionName = "UpdatePositioning()",
		description = "Updates the size and positioning of the Ultimate Joystick. This function can be used to update any options that may have been changed prior to Start().",
		codeExample = "joystick.joystickSize = 4.0f;\njoystick.UpdatePositioning();"
	};
	DocumentationInfo p_GetHorizontalAxis = new DocumentationInfo()
	{
		functionName = "GetHorizontalAxis()",
		showMore = new AnimBool( false ),
		returnType = "float",
		description = "Returns the current horizontal value of the joystick's position. The value returned will always be between -1 and 1, with 0 being the neutral position.",
		codeExample = "float h = joystick.GetHorizontalAxis();"
	};
	DocumentationInfo p_GetVerticalAxis = new DocumentationInfo()
	{
		functionName = "GetVerticalAxis()",
		showMore = new AnimBool( false ),
		returnType = "float",
		description = "Returns the current vertical value of the joystick's position. The value returned will always be between -1 and 1, with 0 being the neutral position.",
		codeExample = "float v = joystick.GetVerticalAxis();"
	};
	DocumentationInfo p_GetHorizontalAxisRaw = new DocumentationInfo()
	{
		functionName = "GetHorizontalAxisRaw()",
		showMore = new AnimBool( false ),
		returnType = "float",
		description = "Returns a value of -1, 0 or 1 representing the raw horizontal value of the Ultimate Joystick.",
		codeExample = "float h = joystick.GetHorizontalAxisRaw();"
	};
	DocumentationInfo p_GetVerticalAxisRaw = new DocumentationInfo()
	{
		functionName = "GetVerticalAxisRaw()",
		showMore = new AnimBool( false ),
		returnType = "float",
		description = "Returns a value of -1, 0 or 1 representing the raw vertical value of the Ultimate Joystick.",
		codeExample = "float v = joystick.GetVerticalAxisRaw();"
	};
	DocumentationInfo p_HorizontalAxis = new DocumentationInfo()
	{
		functionName = "HorizontalAxis",
		showMore = new AnimBool( false ),
		returnType = "float",
		description = "Returns the current horizontal value of the joystick's position. This is a float variable that can be referenced from Game Making Plugins because it is an exposed variable.",
	};
	DocumentationInfo p_VerticalAxis = new DocumentationInfo()
	{
		functionName = "VerticalAxis",
		showMore = new AnimBool( false ),
		returnType = "float",
		description = "Returns the current vertical value of the joystick's position. This is a float variable that can be referenced from Game Making Plugins because it is an exposed variable.",
	};
	DocumentationInfo p_GetDistance = new DocumentationInfo()
	{
		functionName = "GetDistance()",
		showMore = new AnimBool( false ),
		returnType = "float",
		description = "Returns the distance of the joystick from it's center in a float value. The value returned will always be a value between 0 and 1.",
		codeExample = "float dist = joystick.GetDistance();"
	};
	DocumentationInfo p_UpdateHighlightColor = new DocumentationInfo()
	{
		functionName = "UpdateHighlightColor()",
		showMore = new AnimBool( false ),
		parameter = new string[ 1 ]
		{ 
			"Color targetColor - The color to apply to the highlight images."
		},
		description = "Updates the colors of the assigned highlight images with the targeted color if the showHighlight variable is set to true. The targetColor variable will overwrite the current color setting for highlightColor and apply immediately to the highlight images.",
		codeExample = "joystick.UpdateHighlightColor( Color.white );"
	};
	DocumentationInfo p_UpdateTensionColors = new DocumentationInfo()
	{
		functionName = "UpdateTensionColors()",
		showMore = new AnimBool( false ),
		parameter = new string[ 2 ]
		{
			"Color targetTensionNone - The color to apply to the default state of the tension accent image.",
			"Color targetTensionFull - The colors to apply to the touched state of the tension accent images."
		},
		description = "Updates the tension accent image colors with the targeted colors if the showTension variable is true. The tension colors will be set to the targeted colors, and will be applied when the joystick is next used.",
		codeExample = "joystick.UpdateTensionColors( Color.white, Color.green );"
	};
	DocumentationInfo p_GetJoystickState = new DocumentationInfo()
	{
		functionName = "GetJoystickState()",
		showMore = new AnimBool( false ),
		returnType = "bool",
		description = "Returns the state that the joystick is currently in. This function will return true when the joystick is being interacted with, and false when not.",
		codeExample = "if( joystick.GetJoystickState() )\n{\n    Debug.Log( \"The user is interacting with the Ultimate Joystick!\" );\n}"
	};
	DocumentationInfo p_GetTapCount = new DocumentationInfo()
	{
		functionName = "GetTapCount()",
		showMore = new AnimBool( false ),
		returnType = "bool",
		description = "Returns the current state of the joystick's Tap Count according to the options set. The boolean returned will be true only after the Tap Count options have been achieved from the users input.",
		codeExample = "if( joystick.GetTapCount() )\n{\n    Debug.Log( \"The user has achieved the Tap Count!\" );\n}"
	};
	DocumentationInfo p_DisableJoystick = new DocumentationInfo()
	{
		functionName = "DisableJoystick()",
		showMore = new AnimBool( false ),
		description = "This function will reset the Ultimate Joystick and disable the gameObject. Use this function when wanting to disable the Ultimate Joystick from being used.",
		codeExample = "joystick.DisableJoystick();"
	};
	DocumentationInfo p_EnableJoystick = new DocumentationInfo()
	{
		functionName = "EnableJoystick()",
		showMore = new AnimBool( false ),
		description = "This function will ensure that the Ultimate Joystick is completely reset before enabling itself to be used again.",
		codeExample = "joystick.EnableJoystick();"
	};
	
	// STATIC //
	DocumentationInfo s_GetUltimateJoystick = new DocumentationInfo()
	{
		functionName = "GetUltimateJoystick()",
		showMore = new AnimBool( false ),
		parameter = new string[ 1 ]
		{
			"string joystickName - The name that the targeted Ultimate Joystick has been registered with."
		},
		returnType = "UltimateJoystick",
		description = "Returns the Ultimate Joystick component that has been registered with the targeted joystick name.",
		codeExample = "UltimateJoystick moveJoystick = UltimateJoystick.GetUltimateJoystick( \"Movement\" );"
	};
	DocumentationInfo s_GetHorizontalAxis = new DocumentationInfo()
	{
		functionName = "GetHorizontalAxis()",
		showMore = new AnimBool( false ),
		parameter = new string[ 1 ]
		{
			"string joystickName - The name that the targeted Ultimate Joystick has been registered with."
		},
		returnType = "float",
		description = "Returns the current horizontal value of the targeted joystick's position. The value returned will always be between -1 and 1, with 0 being the neutral position.",
		codeExample = "float h = UltimateJoystick.GetHorizontalAxis( \"Movement\" );"
	};
	DocumentationInfo s_GetVerticalAxis = new DocumentationInfo()
	{
		functionName = "GetVerticalAxis()",
		showMore = new AnimBool( false ),
		parameter = new string[ 1 ]
		{
			"string joystickName - The name that the targeted Ultimate Joystick has been registered with."
		},
		returnType = "float",
		description = "Returns the current vertical value of the targeted joystick's position. The value returned will always be between -1 and 1, with 0 being the neutral position.",
		codeExample = "float v = UltimateJoystick.GetVerticalAxis( \"Movement\" );"
	};
	DocumentationInfo s_GetHorizontalAxisRaw = new DocumentationInfo()
	{
		functionName = "GetHorizontalAxisRaw()",
		showMore = new AnimBool( false ),
		parameter = new string[ 1 ]
		{
			"string joystickName - The name that the targeted Ultimate Joystick has been registered with."
		},
		returnType = "float",
		description = "Returns a value of -1, 0 or 1 representing the raw horizontal value of the targeted Ultimate Joystick.",
		codeExample = "float h = UltimateJoystick.GetHorizontalAxisRaw( \"Movement\" );"
	};
	DocumentationInfo s_GetVerticalAxisRaw = new DocumentationInfo()
	{
		functionName = "GetVerticalAxisRaw()",
		showMore = new AnimBool( false ),
		parameter = new string[ 1 ]
		{
			"string joystickName - The name that the targeted Ultimate Joystick has been registered with."
		},
		returnType = "float",
		description = "Returns a value of -1, 0 or 1 representing the raw vertical value of the targeted Ultimate Joystick.",
		codeExample = "float v = UltimateJoystick.GetVerticalAxisRaw( \"Movement\" );"
	};
	DocumentationInfo s_GetDistance = new DocumentationInfo()
	{
		functionName = "GetDistance()",
		showMore = new AnimBool( false ),
		parameter = new string[ 1 ]
		{
			"string joystickName - The name that the targeted Ultimate Joystick has been registered with."
		},
		returnType = "float",
		description = "Returns the distance of the joystick from it's center in a float value. This static function will return the same value as the local GetDistance function.",
		codeExample = "float dist = UltimateJoystick.GetDistance( \"Movement\" );"
	};
	DocumentationInfo s_GetJoystickState = new DocumentationInfo()
	{
		functionName = "GetJoystickState()",
		showMore = new AnimBool( false ),
		parameter = new string[ 1 ]
		{
			"string joystickName - The name that the targeted Ultimate Joystick has been registered with."
		},
		returnType = "bool",
		description = "Returns the state that the joystick is currently in. This function will return true when the joystick is being interacted with, and false when not.",
		codeExample = "if( UltimateJoystick.GetJoystickState( \"Movement\" ) )\n{\n    Debug.Log( \"The user is interacting with the Ultimate Joystick!\" );\n}"
	};
	DocumentationInfo s_GetTapCount = new DocumentationInfo()
	{
		functionName = "GetTapCount()",
		showMore = new AnimBool( false ),
		parameter = new string[ 1 ]
		{
			"string joystickName - The name that the targeted Ultimate Joystick has been registered with."
		},
		returnType = "bool",
		description = "Returns the current state of the joystick's Tap Count according to the options set. The boolean returned will be true only after the Tap Count options have been achieved from the users input.",
		codeExample = "if( UltimateJoystick.GetTapCount( \"Movement\" ) )\n{\n    Debug.Log( \"The user has achieved the Tap Count!\" );\n}"
	};
	DocumentationInfo s_DisableJoystick = new DocumentationInfo()
	{
		functionName = "DisableJoystick()",
		showMore = new AnimBool( false ),
		parameter = new string[ 1 ]
		{
			"string joystickName - The name that the targeted Ultimate Joystick has been registered with."
		},
		description = "This function will reset the Ultimate Joystick and disable the gameObject. Use this function when wanting to disable the Ultimate Joystick from being used.",
		codeExample = "UltimateJoystick.DisableJoystick( \"Movement\" );"
	};
	DocumentationInfo s_EnableJoystick = new DocumentationInfo()
	{
		functionName = "EnableJoystick()",
		showMore = new AnimBool( false ),
		parameter = new string[ 1 ]
		{
			"string joystickName - The name that the targeted Ultimate Joystick has been registered with."
		},
		description = "This function will ensure that the Ultimate Joystick is completely reset before enabling itself to be used again.",
		codeExample = "UltimateJoystick.EnableJoystick( \"Movement\" );"
	};
	#endregion

	[MenuItem( "Window/Tank and Healer Studio/Ultimate Joystick", false, 0 )]
	static void InitializeWindow ()
	{
		EditorWindow window = GetWindow<UltimateJoystickWindow>( true, "Tank and Healer Studio Asset Window", true );
		window.maxSize = new Vector2( 500, 500 );
		window.minSize = new Vector2( 500, 500 );
		window.Show();
	}

	public static void OpenDocumentation ()
	{
		InitializeWindow();

		if( !pageHistory.Contains( documentation ) )
			NavigateForward( documentation );
	}

	void OnEnable ()
	{
		style = ( GUISkin )UnityEngine.Resources.Load( "UltimateJoystickEditorSkin" );

		scriptReference = ( Texture2D )UnityEngine.Resources.Load( "UJ_ScriptRef" );
		ubPromo = ( Texture2D )UnityEngine.Resources.Load( "UB_Promo" );
		usbPromo = ( Texture2D )UnityEngine.Resources.Load( "USB_Promo" );

		if( !pageHistory.Contains( mainMenu ) )
			pageHistory.Insert( 0, mainMenu );

		mainMenu.targetMethod = MainMenu;
		howTo.targetMethod = HowTo;
		overview.targetMethod = OverviewPage;
		documentation.targetMethod = DocumentationPage;
		extras.targetMethod = Extras;
		otherProducts.targetMethod = OtherProducts;
		feedback.targetMethod = Feedback;
		changeLog.targetMethod = ChangeLog;
		versionChanges.targetMethod = VersionChanges;
		thankYou.targetMethod = ThankYou;
		settings.targetMethod = WindowSettings;

		if( pageHistory.Count == 1 )
			currentPage = mainMenu;
	}
	
	void OnGUI ()
	{
		if( style == null )
		{
			GUILayout.BeginVertical( "Box" );
			GUILayout.FlexibleSpace();
			ErrorScreen();
			GUILayout.FlexibleSpace();
			EditorGUILayout.EndVertical();
			return;
		}

		GUI.skin = style;

		paragraphStyle = GUI.skin.GetStyle( "ParagraphStyle" );
		itemHeaderStyle = GUI.skin.GetStyle( "ItemHeader" );
		sectionHeaderStyle = GUI.skin.GetStyle( "SectionHeader" );

		if( !configuredFontSize )
		{
			configuredFontSize = true;
			if( paragraphStyle.fontSize == 14 )
				fontSize = FontSize.Large;
			else if( paragraphStyle.fontSize == 12 )
				fontSize = FontSize.Medium;
			else
				fontSize = FontSize.Small;
		}
		
		GUILayout.BeginVertical( "Box" );
		
		EditorGUILayout.BeginHorizontal();

		EditorGUILayout.LabelField( "Ultimate Joystick", GUI.skin.GetStyle( "WindowTitle" ) );

		if( GUILayout.Button( "", GUI.skin.GetStyle( "SettingsButton" ) ) && currentPage != settings && !pageHistory.Contains( settings ) )
			NavigateForward( settings );

		var rect = GUILayoutUtility.GetLastRect();
		EditorGUIUtility.AddCursorRect( rect, MouseCursor.Link );

		EditorGUILayout.EndHorizontal();

		GUILayout.Space( 3 );
		
		if( GUILayout.Button( "Version " + version, GUI.skin.GetStyle( "VersionNumber" ) ) && currentPage != changeLog && !pageHistory.Contains( changeLog ) )
			NavigateForward( changeLog );
		rect = GUILayoutUtility.GetLastRect();
		EditorGUIUtility.AddCursorRect( rect, MouseCursor.Link );

		GUILayout.Space( 12 );

		EditorGUILayout.BeginHorizontal();
		GUILayout.Space( 5 );
		if( pageHistory.Count > 1 )
		{
			if( GUILayout.Button( "", GUI.skin.GetStyle( "BackButton" ), GUILayout.Width( 80 ), GUILayout.Height( 40 ) ) )
				NavigateBack();
			rect = GUILayoutUtility.GetLastRect();
			EditorGUIUtility.AddCursorRect( rect, MouseCursor.Link );
		}
		else
			GUILayout.Space( 80 );

		GUILayout.Space( 15 );
		EditorGUILayout.LabelField( menuTitle, GUI.skin.GetStyle( "MenuTitle" ) );
		GUILayout.FlexibleSpace();
		GUILayout.Space( 80 );
		EditorGUILayout.EndHorizontal();

		GUILayout.Space( 10 );

		EditorGUILayout.BeginHorizontal();
		GUILayout.FlexibleSpace();
		
		if( currentPage.targetMethod != null )
			currentPage.targetMethod();

		GUILayout.FlexibleSpace();
		EditorGUILayout.EndHorizontal();

		GUILayout.FlexibleSpace();

		GUILayout.Space( 25 );
		
		EditorGUILayout.EndVertical();

		Repaint();
	}

	void ErrorScreen ()
	{
		EditorGUILayout.BeginHorizontal();
		GUILayout.FlexibleSpace();
		GUIStyle errorStyle = new GUIStyle( GUI.skin.label );
		errorStyle.fixedHeight = 55;
		errorStyle.fixedWidth = 175;
		errorStyle.fontSize = 48;
		errorStyle.normal.textColor = new Color( 1.0f, 0.0f, 0.0f, 1.0f );
		EditorGUILayout.LabelField( "ERROR", errorStyle );
		GUILayout.FlexibleSpace();
		EditorGUILayout.EndHorizontal();

		GUILayout.Space( 50 );

		EditorGUILayout.BeginHorizontal();
		GUILayout.Space( 50 );
		EditorGUILayout.LabelField( "Could not find the needed GUISkin located in the Editor / Resources folder. Please ensure that the correct GUISkin, UltimateJoystickEditorSkin, is in the right folder( Ultimate Joystick / Editor / Resources ) before trying to access the Ultimate Joystick Window.", EditorStyles.wordWrappedLabel );
		GUILayout.Space( 50 );
		EditorGUILayout.EndHorizontal();
	}

	static void NavigateBack ()
	{
		pageHistory.RemoveAt( pageHistory.Count - 1 );
		menuTitle = pageHistory[ pageHistory.Count - 1 ].pageName;
		currentPage = pageHistory[ pageHistory.Count - 1 ];
	}

	static void NavigateForward ( PageInformation menu )
	{
		pageHistory.Add( menu );
		menuTitle = menu.pageName;
		currentPage = menu;
	}
	
	void MainMenu ()
	{
		mainMenu.scrollPosition = EditorGUILayout.BeginScrollView( mainMenu.scrollPosition, false, false, docSize );

		GUILayout.Space( 25 );
		EditorGUILayout.BeginHorizontal();
		GUILayout.FlexibleSpace();
		if( GUILayout.Button( "How To", buttonSize ) )
			NavigateForward( howTo );

		var rect = GUILayoutUtility.GetLastRect();
		EditorGUIUtility.AddCursorRect( rect, MouseCursor.Link );

		GUILayout.FlexibleSpace();
		EditorGUILayout.EndHorizontal();

		GUILayout.FlexibleSpace();

		EditorGUILayout.BeginHorizontal();
		GUILayout.FlexibleSpace();
		if( GUILayout.Button( "Overview", buttonSize ) )
			NavigateForward( overview );
		
		rect = GUILayoutUtility.GetLastRect();
		EditorGUIUtility.AddCursorRect( rect, MouseCursor.Link );

		GUILayout.FlexibleSpace();
		EditorGUILayout.EndHorizontal();

		GUILayout.FlexibleSpace();

		EditorGUILayout.BeginHorizontal();
		GUILayout.FlexibleSpace();
		if( GUILayout.Button( "Documentation", buttonSize ) )
			NavigateForward( documentation );
		
		rect = GUILayoutUtility.GetLastRect();
		EditorGUIUtility.AddCursorRect( rect, MouseCursor.Link );

		GUILayout.FlexibleSpace();
		EditorGUILayout.EndHorizontal();

		GUILayout.FlexibleSpace();

		EditorGUILayout.BeginHorizontal();
		GUILayout.FlexibleSpace();
		if( GUILayout.Button( "Extras", buttonSize ) )
			NavigateForward( extras );
		
		rect = GUILayoutUtility.GetLastRect();
		EditorGUIUtility.AddCursorRect( rect, MouseCursor.Link );

		GUILayout.FlexibleSpace();
		EditorGUILayout.EndHorizontal();

		GUILayout.FlexibleSpace();

		EditorGUILayout.BeginHorizontal();
		GUILayout.FlexibleSpace();
		if( GUILayout.Button( "Other Products", buttonSize ) )
			NavigateForward( otherProducts );
		
		rect = GUILayoutUtility.GetLastRect();
		EditorGUIUtility.AddCursorRect( rect, MouseCursor.Link );

		GUILayout.FlexibleSpace();
		EditorGUILayout.EndHorizontal();

		GUILayout.FlexibleSpace();

		EditorGUILayout.BeginHorizontal();
		GUILayout.FlexibleSpace();
		if( GUILayout.Button( "Feedback", buttonSize ) )
			NavigateForward( feedback );
		
		rect = GUILayoutUtility.GetLastRect();
		EditorGUIUtility.AddCursorRect( rect, MouseCursor.Link );

		GUILayout.FlexibleSpace();
		EditorGUILayout.EndHorizontal();

		GUILayout.FlexibleSpace();

		EditorGUILayout.EndScrollView();
	}

	void HowTo ()
	{
		StartPage( howTo );

		EditorGUILayout.LabelField( "How To Create", sectionHeaderStyle );

		GUILayout.Space( paragraphSpace );

		EditorGUILayout.LabelField( "   To create an Ultimate Joystick in your scene, simply find the Ultimate Joystick prefab that you would like to add and click on the \"Add to Scene\" button on the Ultimate Joystick inspector. What this does is creates that Ultimate Joystick within the scene and ensures that there is a Canvas and an EventSystem so that it can work correctly. If these are not present in the scene, they will be created for you.", paragraphStyle );

		GUILayout.Space( sectionSpace );

		EditorGUILayout.LabelField( "How To Reference", sectionHeaderStyle );

		GUILayout.Space( paragraphSpace );

		EditorGUILayout.LabelField( "   One of the great things about the Ultimate Joystick is how easy it is to reference to other scripts. The first thing that you will want to make sure to do is to name the joystick in the Script Reference section. After this is complete, you will be able to reference that particular joystick by the name that you have assigned to it.", paragraphStyle );

		GUILayout.Space( paragraphSpace );

		EditorGUILayout.LabelField( "After the joystick has been given a name in the Script Reference section, we can get that joystick's position by catching the Horizontal and Vertical values at run time and storing the results from the static functions: <i>GetHorizontalAxis</i> and <i>GetVerticalAxis</i>. These functions will return the joystick's position, and will be float values between -1, and 1, with 0 being at the center. For more information about these functions, and others that are available to the Ultimate Joystick class, please see the Documentation section of this window.", paragraphStyle );
		
		GUILayout.Space( sectionSpace );

		EditorGUILayout.LabelField( "Simple Example", sectionHeaderStyle );

		GUILayout.Space( paragraphSpace );

		EditorGUILayout.LabelField( "   Let's assume that we want to use a joystick for a characters movement. The first thing to do is to assign the name \"Movement\" in the Joystick Name variable located in the Script Reference section of the Ultimate Joystick.", paragraphStyle );
		
		GUILayout.Space( 10 );

		EditorGUILayout.BeginHorizontal();
		GUILayout.FlexibleSpace();
		GUILayout.Label( scriptReference );
		GUILayout.FlexibleSpace();
		EditorGUILayout.EndHorizontal();

		GUILayout.Space( 10 );

		EditorGUILayout.LabelField( "After that, we need to create two float variables to store the result of the joystick's position. In order to get the \"Movement\" joystick's position, we need to pass in the name \"Movement\" as the argument.", paragraphStyle );

		GUILayout.Space( 10 );

		EditorGUILayout.LabelField( "Coding Example:", itemHeaderStyle );
		EditorGUILayout.TextArea( "float h = UltimateJoystick.GetHorizontalAxis( \"Movement\" );\nfloat v = UltimateJoystick.GetVerticalAxis( \"Movement\" );", GUI.skin.GetStyle( "TextArea" ) );
		
		GUILayout.Space( 10 );

		EditorGUILayout.LabelField( "The h and v variables now contain the values of the Movement joystick's position. Now we can use this information in any way that is desired. For example, if you are wanting to put the joystick's position into a character movement script, you would create a Vector3 variable for movement direction, and put in the appropriate values of this joystick's position.", paragraphStyle );

		GUILayout.Space( 10 );

		EditorGUILayout.LabelField( "Coding Example:", itemHeaderStyle );
		EditorGUILayout.TextArea( "Vector3 movementDirection = new Vector3( h, 0, v );", GUI.skin.GetStyle( "TextArea" ) );
		
		GUILayout.Space( 10 );

		EditorGUILayout.LabelField( "In the above example, the h variable is used to in the X slot of the Vector3, and the v variable is in the Z slot. This is because you generally don't want your character to move in the Y direction unless the user jumps. That is why we put the v variable into the Z value of the movementDirection variable.", paragraphStyle );

		GUILayout.Space( 10 );

		EditorGUILayout.LabelField( "Understanding how to use the values from any input is important when creating character controllers, so experiment with the values and try to understand how user input can be used in different ways.", paragraphStyle );

		GUILayout.Space( itemHeaderSpace );

		EndPage();
	}
	
	void OverviewPage ()
	{
		StartPage( overview );
		
		/* //// --------------------------- < SIZE AND PLACEMENT > --------------------------- \\\\ */
		EditorGUILayout.LabelField( "Size And Placement", sectionHeaderStyle );

		GUILayout.Space( paragraphSpace );

		EditorGUILayout.LabelField( "   The Size and Placement section allows you to customize the joystick's size and placement on the screen, as well as determine where the user's touch can be processed for the selected joystick.", paragraphStyle );

		GUILayout.Space( paragraphSpace );

		// Scaling Axis
		EditorGUILayout.LabelField( "Scaling Axis", itemHeaderStyle );
		EditorGUILayout.LabelField( "Determines which axis the joystick will be scaled from. If Height is chosen, then the joystick will scale itself proportionately to the Height of the screen.", paragraphStyle );

		GUILayout.Space( paragraphSpace );

		// Anchor
		EditorGUILayout.LabelField( "Anchor", itemHeaderStyle );
		EditorGUILayout.LabelField( "Determines which side of the screen that the joystick will be anchored to.", paragraphStyle );

		GUILayout.Space( paragraphSpace );

		// Touch Size
		EditorGUILayout.LabelField( "Touch Size", itemHeaderStyle );
		EditorGUILayout.LabelField( "Touch Size configures the size of the area where the user can touch. You have the options of either <i>Default, Medium, Large or Custom</i>.", paragraphStyle );

		GUILayout.Space( paragraphSpace );

		// Touch Size Customization
		EditorGUILayout.LabelField( "Touch Size Customization", itemHeaderStyle );
		EditorGUILayout.LabelField( "If the <i>Custom</i> option of the Touch Size is selected, then you will be presented with the Touch Size Customization box. Inside this box are settings for the Width and Height of the touch area, as well as the X and Y position of the touch area on the screen.", paragraphStyle );

		GUILayout.Space( paragraphSpace );

		// Dynamic Positioning
		EditorGUILayout.LabelField( "Dynamic Positioning", itemHeaderStyle );
		EditorGUILayout.LabelField( "Dynamic Positioning will make the joystick snap to where the user touches, instead of the user having to touch a direct position to get the joystick. The Touch Size option will directly affect the area where the joystick can snap to.", paragraphStyle );

		GUILayout.Space( paragraphSpace );

		// Joystick Size
		EditorGUILayout.LabelField( "Joystick Size", itemHeaderStyle );
		EditorGUILayout.LabelField( "Joystick Size will change the scale of the joystick. Since everything is calculated out according to screen size, your joystick Touch Size and other properties will scale proportionately with the joystick's size along your specified Scaling Axis.", paragraphStyle );

		GUILayout.Space( paragraphSpace );

		// Radius
		EditorGUILayout.LabelField( "Radius", itemHeaderStyle );
		EditorGUILayout.LabelField( "Radius determines how far away the joystick will move from center when it is being used, and will scale proportionately with the joystick.", paragraphStyle );

		GUILayout.Space( paragraphSpace );

		// Joystick Position
		EditorGUILayout.LabelField( "Joystick Position", itemHeaderStyle );
		EditorGUILayout.LabelField( "Joystick Position will present you with two sliders. The X value will determine how far the Joystick is away from the Left and Right sides of the screen, and the Y value from the Top and Bottom. This will encompass 50% of your screen width, relevant to your Anchor selection.", paragraphStyle );
		/* \\\\ -------------------------- < END SIZE AND PLACEMENT > --------------------------- //// */
		
		GUILayout.Space( sectionSpace );

		/* //// ----------------------------- < STYLE AND OPTIONS > ----------------------------- \\\\ */
		EditorGUILayout.LabelField( "Style And Options", sectionHeaderStyle );

		GUILayout.Space( paragraphSpace );

		EditorGUILayout.LabelField( "   The Style and Options section contains options that affect how the joystick handles and is visually presented to the user.", paragraphStyle );

		GUILayout.Space( paragraphSpace );

		// -----< VISUAL DISPLAY >----- //

		// Disable Visuals
		EditorGUILayout.LabelField( "Disable Visuals", itemHeaderStyle );
		EditorGUILayout.LabelField( "Disable Visuals presents you with the option to disable the visuals of the joystick, whilst keeping all functionality. When paired with Dynamic Positioning and Throwable, this option can give you a very smooth camera control.", paragraphStyle );

		GUILayout.Space( paragraphSpace );

		// Use Fade
		EditorGUILayout.LabelField( "Use Fade", itemHeaderStyle );
		EditorGUILayout.LabelField( "The Use Fade option allows you to set the visibility of the joystick to display the current interaction state. You can also customize the duration for the fade between the targeted alpha settings.", paragraphStyle );

		GUILayout.Space( paragraphSpace );

		// Use Animation
		EditorGUILayout.LabelField( "Use Animation", itemHeaderStyle );
		EditorGUILayout.LabelField( "If you would like the joystick to play an animation when being interacted with, then you will want to enable the Use Animation option.", paragraphStyle );

		GUILayout.Space( paragraphSpace );

		// Show Highlight
		EditorGUILayout.LabelField( "Show Highlight", itemHeaderStyle );
		EditorGUILayout.LabelField( "Show Highlight will allow you to customize the highlight images with a custom color. With this option, you will also be able to customize and set the highlight color at runtime using the <i>UpdateHighlightColor</i> function.", paragraphStyle );

		GUILayout.Space( paragraphSpace );

		// Show Tension
		EditorGUILayout.LabelField( "Show Tension", itemHeaderStyle );
		EditorGUILayout.LabelField( "With Show Tension enabled, the joystick will display it's position visually. This is done using custom colors and images that will display the direction and intensity of the joystick's current position. With this option enabled, you will be able to update the tension colors at runtime using the <i>UpdateTensionColors</i> function.", paragraphStyle );

		GUILayout.Space( paragraphSpace );

		// -----< FUNCTIONALITY >----- //

		// Throwable
		EditorGUILayout.LabelField( "Throwable", itemHeaderStyle );
		EditorGUILayout.LabelField( "The Throwable option allows the joystick to smoothly transition back to center after being released. This can be used to give your input a smoother feel.", paragraphStyle );

		GUILayout.Space( paragraphSpace );

		// Draggable
		EditorGUILayout.LabelField( "Draggable", itemHeaderStyle );
		EditorGUILayout.LabelField( "The Draggable option will allow the joystick to move from it's default position when the user's input exceeds the set radius.", paragraphStyle );

		GUILayout.Space( paragraphSpace );

		// Axis
		EditorGUILayout.LabelField( "Axis", itemHeaderStyle );
		EditorGUILayout.LabelField( "Axis determines which axis the joystick will snap to. By default it is set to Both, which means the joystick will use both the X and Y axis for movement. If either the X or Y option is selected, then the joystick will lock to the corresponding axis.", paragraphStyle );

		GUILayout.Space( paragraphSpace );

		// Dead Zone
		EditorGUILayout.LabelField( "Dead Zone", itemHeaderStyle );
		EditorGUILayout.LabelField( "Dead Zone allows you to set the size of the dead zone on the Ultimate Joystick. All movement within this value will map to neutral.", paragraphStyle );

		GUILayout.Space( paragraphSpace );

		// Tap Count
		EditorGUILayout.LabelField( "Tap Count", itemHeaderStyle );
		EditorGUILayout.LabelField( "The Tap Count option allows you to decide if you want to store the amount of taps that the joystick receives. The options provided with the Tap Count will allow you to customize the target amount of taps and the amount of time the user will have to accumulate these taps.", paragraphStyle );
		/* //// --------------------------- < END STYLE AND OPTIONS > --------------------------- \\\\ */

		GUILayout.Space( sectionSpace );

		/* //// ----------------------------- < SCRIPT REFERENCE > ------------------------------ \\\\ */
		EditorGUILayout.LabelField( "Script Reference", sectionHeaderStyle );

		GUILayout.Space( paragraphSpace );
		
		EditorGUILayout.LabelField( "   The Script Reference section contains fields for naming and helpful code snippets that you can copy and paste into your scripts.", paragraphStyle );

		GUILayout.Space( paragraphSpace );

		// Joystick Name
		EditorGUILayout.LabelField( "Joystick Name", itemHeaderStyle );
		EditorGUILayout.LabelField( "The unique name of the selected Ultimate Joystick. This name is what will be used to reference this particular joystick from the public static functions.", paragraphStyle );

		GUILayout.Space( paragraphSpace );
		
		EditorGUILayout.LabelField( "Function", itemHeaderStyle );
		EditorGUILayout.LabelField( "This option will present you with a code snippet that is determined by your selection. This code can be copy and pasted into your custom scripts. Please note that the Function option does not actually determine what the joystick can do. Instead it only provides example code for you to use in your scripts.", paragraphStyle );

		GUILayout.Space( paragraphSpace );
		
		EditorGUILayout.LabelField( "Current Position", itemHeaderStyle );
		EditorGUILayout.LabelField( "This box simply displays the Ultimate Joystick's current position. This is only useful for debugging.", paragraphStyle );

		GUILayout.Space( itemHeaderSpace );

		EndPage();
	}
	
	void DocumentationPage ()
	{
		StartPage( documentation );

		/* //// --------------------------- < PUBLIC FUNCTIONS > --------------------------- \\\\ */
		EditorGUILayout.LabelField( "Public Functions", sectionHeaderStyle );

		GUILayout.Space( paragraphSpace );

		EditorGUILayout.LabelField( Indent + "All of the following public functions are only available from a reference to the Ultimate Joystick. Each example provided relies on having a Ultimate Joystick variable named 'joystick' stored inside your script. When using any of the example code provided, make sure that you have a public Ultimate Joystick variable like the one below:", paragraphStyle );

		EditorGUILayout.TextArea( "public UltimateJoystick joystick;", GUI.skin.textArea );

		EditorGUILayout.LabelField( "Please click on the function name to learn more.", paragraphStyle );

		GUILayout.Space( paragraphSpace );

		ShowDocumentation( p_UpdatePositioning );
		ShowDocumentation( p_GetHorizontalAxis );
		ShowDocumentation( p_GetVerticalAxis );
		ShowDocumentation( p_GetHorizontalAxisRaw );
		ShowDocumentation( p_GetVerticalAxisRaw );
		ShowDocumentation( p_HorizontalAxis );
		ShowDocumentation( p_VerticalAxis );
		ShowDocumentation( p_GetDistance );
		ShowDocumentation( p_UpdateHighlightColor );
		ShowDocumentation( p_UpdateTensionColors );
		ShowDocumentation( p_GetJoystickState );
		ShowDocumentation( p_GetTapCount );
		ShowDocumentation( p_DisableJoystick );
		ShowDocumentation( p_EnableJoystick );

		GUILayout.Space( sectionSpace );
		
		/* //// --------------------------- < STATIC FUNCTIONS > --------------------------- \\\\ */
		EditorGUILayout.LabelField( "Static Functions", sectionHeaderStyle );

		GUILayout.Space( paragraphSpace );

		EditorGUILayout.LabelField( Indent + "The following functions can be referenced from your scripts without the need for an assigned local Ultimate Joystick variable. However, each function must have the targeted Ultimate Joystick name in order to find the correct Ultimate Joystick in the scene. Each example code provided uses the name 'Movement' as the joystick name.", paragraphStyle );

		GUILayout.Space( paragraphSpace );

		ShowDocumentation( s_GetUltimateJoystick );
		ShowDocumentation( s_GetHorizontalAxis );
		ShowDocumentation( s_GetVerticalAxis );
		ShowDocumentation( s_GetHorizontalAxisRaw );
		ShowDocumentation( s_GetVerticalAxisRaw );
		ShowDocumentation( s_GetDistance );
		ShowDocumentation( s_GetJoystickState );
		ShowDocumentation( s_GetTapCount );
		ShowDocumentation( s_DisableJoystick );
		ShowDocumentation( s_EnableJoystick );
		
		GUILayout.Space( itemHeaderSpace );
		
		EndPage();
	}
	
	void Extras ()
	{
		StartPage( extras );

		EditorGUILayout.LabelField( "Videos", sectionHeaderStyle );
		EditorGUILayout.LabelField( "   The links below are to the collection of videos that we have made in connection with the Ultimate Joystick. The Tutorial Videos are designed to get the Ultimate Joystick implemented into your project as fast as possible, and give you a good understanding of what you can achieve using it in your projects, whereas the demonstrations are videos showing how we, and others in the Unity community, have used assets created by Tank & Healer Studio in our projects.", paragraphStyle );

		GUILayout.Space( 10 );

		EditorGUILayout.BeginHorizontal();
		GUILayout.FlexibleSpace();
		if( GUILayout.Button( "Tutorials", buttonSize ) )
			Application.OpenURL( "https://www.youtube.com/playlist?list=PL7crd9xMJ9TmWdbR_bklluPeElJ_xUdO9" );
		
		var rect = GUILayoutUtility.GetLastRect();
		EditorGUIUtility.AddCursorRect( rect, MouseCursor.Link );

		GUILayout.FlexibleSpace();
		EditorGUILayout.EndHorizontal();

		GUILayout.Space( 10 );

		EditorGUILayout.BeginHorizontal();
		GUILayout.FlexibleSpace();
		if( GUILayout.Button( "Demonstrations", buttonSize ) )
			Application.OpenURL( "https://www.youtube.com/playlist?list=PL7crd9xMJ9TlkjepDAY_GnpA1CX-rFltz" );
		
		rect = GUILayoutUtility.GetLastRect();
		EditorGUIUtility.AddCursorRect( rect, MouseCursor.Link );

		GUILayout.FlexibleSpace();
		EditorGUILayout.EndHorizontal();

		EndPage();
	}
	
	void OtherProducts ()
	{
		StartPage( otherProducts );

		/* -------------- < ULTIMATE BUTTON > -------------- */
		if( ubPromo != null )
		{
			EditorGUILayout.BeginHorizontal();
			GUILayout.FlexibleSpace();
			GUILayout.Space( 15 );
			GUILayout.Label( ubPromo, GUILayout.Width( 250 ), GUILayout.Height( 125 ) );
			GUILayout.FlexibleSpace();
			EditorGUILayout.EndHorizontal();

			GUILayout.Space( paragraphSpace );
		}

		EditorGUILayout.LabelField( "Ultimate Button", sectionHeaderStyle );

		EditorGUILayout.LabelField( "   Buttons are a core element of UI, and as such they should be easy to customize and implement. The Ultimate Button is the embodiment of that very idea. This code package takes the best of Unity's Input and UnityEvent methods and pairs it with exceptional customization to give you the most versatile button for your mobile project. Are you in need of a button for attacking, jumping, shooting, or all of the above? With Ultimate Button's easy size and placement options, style options, script reference and button events, you'll have everything you need to create your custom buttons, whether they are simple or complex.", paragraphStyle );

		GUILayout.Space( 10 );

		EditorGUILayout.BeginHorizontal();
		GUILayout.FlexibleSpace();
		if( GUILayout.Button( "More Info", buttonSize ) )
			Application.OpenURL( "http://www.tankandhealerstudio.com/ultimate-button.html" );
		
		var rect = GUILayoutUtility.GetLastRect();
		EditorGUIUtility.AddCursorRect( rect, MouseCursor.Link );

		GUILayout.FlexibleSpace();
		EditorGUILayout.EndHorizontal();
		/* ------------ < END ULTIMATE BUTTON > ------------ */

		GUILayout.Space( 25 );

		/* ------------ < ULTIMATE STATUS BAR > ------------ */
		if( usbPromo != null )
		{
			EditorGUILayout.BeginHorizontal();
			GUILayout.FlexibleSpace();
			GUILayout.Space( 15 );
			GUILayout.Label( usbPromo, GUILayout.Width( 250 ), GUILayout.Height( 125 ) );
			GUILayout.FlexibleSpace();
			EditorGUILayout.EndHorizontal();

			GUILayout.Space( paragraphSpace );
		}

		EditorGUILayout.LabelField( "Ultimate Status Bar", sectionHeaderStyle );

		EditorGUILayout.LabelField( "   The Ultimate Status Bar is a complete solution to display virtually any status for your game. With an abundance of options and customization available to you, as well as the simplest integration, the Ultimate Status Bar makes displaying any condition a cinch. Whether it’s health and energy for your player, the health of a target, or the progress of loading your scene, the Ultimate Status Bar can handle it with ease and style!", paragraphStyle );

		GUILayout.Space( 10 );

		EditorGUILayout.BeginHorizontal();
		GUILayout.FlexibleSpace();
		if( GUILayout.Button( "More Info", buttonSize ) )
			Application.OpenURL( "http://www.tankandhealerstudio.com/ultimate-status-bar.html" );
		
		rect = GUILayoutUtility.GetLastRect();
		EditorGUIUtility.AddCursorRect( rect, MouseCursor.Link );

		GUILayout.FlexibleSpace();
		EditorGUILayout.EndHorizontal();
		/* -------------- < END STATUS BAR > --------------- */

		EndPage();
	}
	
	void Feedback ()
	{
		StartPage( feedback );

		EditorGUILayout.LabelField( "Having Problems?", sectionHeaderStyle );

		EditorGUILayout.LabelField( "   If you experience any issues with the Ultimate Joystick, please contact us right away! We will lend any assistance that we can to resolve any issues that you have.\n\n<b>Support Email:</b>.", paragraphStyle );

		GUILayout.Space( paragraphSpace );
		EditorGUILayout.SelectableLabel( "tankandhealerstudio@outlook.com", itemHeaderStyle, GUILayout.Height( 15 ) );
		GUILayout.Space( 25 );


		EditorGUILayout.LabelField( "Good Experiences?", sectionHeaderStyle );

		EditorGUILayout.LabelField( "   If you have appreciated how easy the Ultimate Joystick is to get into your project, leave us a comment and rating on the Unity Asset Store. We are very grateful for all positive feedback that we get.", paragraphStyle );

		GUILayout.Space( 10 );

		EditorGUILayout.BeginHorizontal();
		GUILayout.FlexibleSpace();
		if( GUILayout.Button( "Rate Us", buttonSize ) )
			Application.OpenURL( "https://www.assetstore.unity3d.com/en/#!/content/27695" );
		
		var rect = GUILayoutUtility.GetLastRect();
		EditorGUIUtility.AddCursorRect( rect, MouseCursor.Link );

		GUILayout.FlexibleSpace();
		EditorGUILayout.EndHorizontal();

		GUILayout.Space( 25 );

		EditorGUILayout.LabelField( "Show Us What You've Done!", sectionHeaderStyle );

		EditorGUILayout.LabelField( "   If you have used any of the assets created by Tank & Healer Studio in your project, we would love to see what you have done. Contact us with any information on your game and we will be happy to support you in any way that we can!\n\n<b>Contact Us:</b>", paragraphStyle );

		GUILayout.Space( paragraphSpace );
		EditorGUILayout.SelectableLabel( "tankandhealerstudio@outlook.com" , itemHeaderStyle, GUILayout.Height( 15 ) );
		GUILayout.Space( itemHeaderSpace );

		EditorGUILayout.BeginHorizontal();
		GUILayout.FlexibleSpace();
		EditorGUILayout.LabelField( "Happy Game Making,\n	-Tank & Healer Studio", GUILayout.Height( 30 ) );
		GUILayout.FlexibleSpace();
		EditorGUILayout.EndHorizontal();

		GUILayout.Space( 25 );

		EndPage();
	}

	void ChangeLog ()
	{
		StartPage( changeLog );

		EditorGUILayout.LabelField( "Version 2.5.1", itemHeaderStyle );
		EditorGUILayout.LabelField( "  • Improved the calculation of the joystick center.", paragraphStyle );
		EditorGUILayout.LabelField( "  • Uploaded with Unity 2018 to show compatibility with all all versions of Unity.", paragraphStyle );

		GUILayout.Space( itemHeaderSpace );

		EditorGUILayout.LabelField( "Version 2.5( Major Update )", itemHeaderStyle );
		EditorGUILayout.LabelField( "  • Reordered folders ( again ) to better conform to Unity's new standard for folder structure. This may cause some errors if you already had the Ultimate Joystick inside of your project. Please <b>completely remove</b> the Ultimate Joystick from your project and re-import the Ultimate Joystick after.", paragraphStyle );
		EditorGUILayout.LabelField( "  • Removed the ability to create an Ultimate Joystick from the Create menu because of the new folder structure. In order to create an Ultimate Joystick in your scene, use the method explained in the How To section of this window.", paragraphStyle );
		EditorGUILayout.LabelField( "  • Fixed a small problem with the Animator for the joysticks in the Space Shooter example scene.", paragraphStyle );
		EditorGUILayout.LabelField( "  • Major improvements to the Ultimate Joystick Editor.", paragraphStyle );
		EditorGUILayout.LabelField( "  • Completely revamped the current Dead Zone option to be more consistent with Unity's default input system.", paragraphStyle );
		EditorGUILayout.LabelField( "  • Updated support for game making plugins by exposing two get values: HoriztonalAxis and VerticalAxis.", paragraphStyle );
		EditorGUILayout.LabelField( "  • Added a new script to handle updating with screen size. The script is named UltimateJoystickScreenSizeUpdater.", paragraphStyle );
		EditorGUILayout.LabelField( "  • Renamed the GetJoystick function to be GetUltimateJoystick.", paragraphStyle );
		EditorGUILayout.LabelField( "  • Added two new functions to use in accord with the new Dead Zone option. These new functions work very similarly to Unity's GetAxisRaw function.\n     • GetHorizontalAxisRaw\n     • GetVerticalAxisRaw", paragraphStyle );
		EditorGUILayout.LabelField( "  • Added an official way to disable and enable the Ultimate Joystick through code.\n     • DisableJoystick\n     • EnableJoystick", paragraphStyle );
		EditorGUILayout.LabelField( "  • Removed the Vector2 GetPosition function. All input values should be obtained through the GetHorizotalAxis and GetVerticalAxis functions.", paragraphStyle );
		
		GUILayout.Space( itemHeaderSpace );

		EditorGUILayout.LabelField( "Version 2.1.5", itemHeaderStyle );
		EditorGUILayout.LabelField( "  • Improvements to the Documentation Window.", paragraphStyle );
		EditorGUILayout.LabelField( "  • Minor editor fixes.", paragraphStyle );

		GUILayout.Space( itemHeaderSpace );

		EditorGUILayout.LabelField( "Version 2.1.4", itemHeaderStyle );
		EditorGUILayout.LabelField( "  • Removed the Third Person Example folder and all it's contents because it was using Unity's scripts, which were causing errors for some who had the Standard Assets in their folders.", paragraphStyle );
		EditorGUILayout.LabelField( "  • Added two new functions to be used for Disabling and Enabling the Ultimate Joystick at runtime. See DisableJoystick() and EnableJoystick() in the Documentation section for more information.", paragraphStyle );
		EditorGUILayout.LabelField( "  • Removed the adding of the Ultimate Joystick Updater script while in the editor as it caused strange errors occasionally.", paragraphStyle );

		GUILayout.Space( itemHeaderSpace );

		EditorGUILayout.LabelField( "Version 2.1.3", itemHeaderStyle );
		EditorGUILayout.LabelField( "  • Updated Documentation Window with up-to-date information, as well as improving overall functionality of the Documentation Window.", paragraphStyle );
		EditorGUILayout.LabelField( "  • Minor editor fixes.", paragraphStyle );

		GUILayout.Space( itemHeaderSpace );

		EditorGUILayout.LabelField( "Version 2.1.2", itemHeaderStyle );
		EditorGUILayout.LabelField( "  • Removed Touch Actions section from the Editor. All options that were previously in the Touch Actions section are now located in the Style and Options section.", paragraphStyle );
		EditorGUILayout.LabelField( "  • Fixed an issue with the Documentation Window not showing up as intended in some rare cases.", paragraphStyle );

		GUILayout.Space( itemHeaderSpace );

		EditorGUILayout.LabelField( "Version 2.1.1", itemHeaderStyle );
		EditorGUILayout.LabelField( "  • Improved functionality for the basic interaction of the Ultimate Joystick.", paragraphStyle );
		EditorGUILayout.LabelField( "  • Minor change to the Ultimate Joystick editor.", paragraphStyle );

		GUILayout.Space( itemHeaderSpace );

		EditorGUILayout.LabelField( "Version 2.1", itemHeaderStyle );
		EditorGUILayout.LabelField( "  • Removed all example files from the Plugins folder.", paragraphStyle );
		EditorGUILayout.LabelField( "  • All example files have been placed in a new folder: Ultimate Joystick Examples.", paragraphStyle );
		EditorGUILayout.LabelField( "  • Added new example scene.", paragraphStyle );
		EditorGUILayout.LabelField( "  • Updated third-person example with more functionality.", paragraphStyle );
		EditorGUILayout.LabelField( "  • Added four new Ultimate Joystick textures.", paragraphStyle );
		EditorGUILayout.LabelField( "  • Improved tension accent display functionality.", paragraphStyle );
		EditorGUILayout.LabelField( "  • Further improved Ultimate Joystick Editor functionality.", paragraphStyle );
		EditorGUILayout.LabelField( "  • Removed Ultimate Joystick PSD from the project files.", paragraphStyle );
		EditorGUILayout.LabelField( "  • Added four new functions to increase the efficiency of referencing the Ultimate Joystick.", paragraphStyle );
		EditorGUILayout.LabelField( "  • Removed unneeded static functions.", paragraphStyle );
		EditorGUILayout.LabelField( "  • Renamed some functions to better reflect their purpose.", paragraphStyle );
		EditorGUILayout.LabelField( "  • Added page to Documentation Window to show important changes.", paragraphStyle );

		GUILayout.Space( itemHeaderSpace );

		EditorGUILayout.LabelField( "Version 2.0.4", itemHeaderStyle );
		EditorGUILayout.LabelField( "  • Minor change to the editor script.", paragraphStyle );

		GUILayout.Space( itemHeaderSpace );

		EditorGUILayout.LabelField( "Version 2.0.3", itemHeaderStyle );
		EditorGUILayout.LabelField( "  • Attempt to upload package without any Thumbs.db files included.", paragraphStyle );

		GUILayout.Space( itemHeaderSpace );

		EditorGUILayout.LabelField( "Version 2.0.2", itemHeaderStyle );
		EditorGUILayout.LabelField( "  • Small fix to the editor window.", paragraphStyle );

		GUILayout.Space( itemHeaderSpace );

		EditorGUILayout.LabelField( "Version 2.0.1", itemHeaderStyle );
		EditorGUILayout.LabelField( "  • Fixed a small issue with the fade not working as intended.", paragraphStyle );

		GUILayout.Space( itemHeaderSpace );

		EditorGUILayout.LabelField( "Version 2.0", itemHeaderStyle );
		EditorGUILayout.LabelField( "  • Added a new In-Engine Documentation Window.", paragraphStyle );
		EditorGUILayout.LabelField( "  • Removed Javascript scripts to improve script reference functionality.", paragraphStyle );
		EditorGUILayout.LabelField( "  • Reorganized folder structure.", paragraphStyle );
		EditorGUILayout.LabelField( "  • Improved fade options within the Touch Actions section.", paragraphStyle );
		EditorGUILayout.LabelField( "  • Improved overall performance.", paragraphStyle );

		EndPage();
	}
	
	void ThankYou ()
	{
		StartPage( thankYou );
		
		EditorGUILayout.LabelField( "    The two of us at Tank & Healer Studio would like to thank you for purchasing the Ultimate Joystick asset package from the Unity Asset Store. If you have any questions about the Ultimate Joystick that are not covered in this Documentation Window, please don't hesitate to contact us at: ", paragraphStyle );

		GUILayout.Space( paragraphSpace );
		EditorGUILayout.SelectableLabel( "tankandhealerstudio@outlook.com" , itemHeaderStyle, GUILayout.Height( 15 ) );
		GUILayout.Space( sectionSpace );

		EditorGUILayout.LabelField( "    We hope that the Ultimate Joystick will be a great help to you in the development of your game. After pressing the continue button below, you will be presented with helpful information on this asset to assist you in implementing Ultimate Joystick into your project.\n", paragraphStyle );

		GUILayout.Space( sectionSpace );

		EditorGUILayout.BeginHorizontal();
		GUILayout.FlexibleSpace();
		EditorGUILayout.LabelField( "Happy Game Making,\n	-Tank & Healer Studio", paragraphStyle, GUILayout.Height( 30 ) );
		GUILayout.FlexibleSpace();
		EditorGUILayout.EndHorizontal();

		GUILayout.Space( 15 );

		EditorGUILayout.BeginHorizontal();
		GUILayout.FlexibleSpace();
		if( GUILayout.Button( "Continue", buttonSize ) )
			NavigateBack();
		
		var rect = GUILayoutUtility.GetLastRect();
		EditorGUIUtility.AddCursorRect( rect, MouseCursor.Link );

		GUILayout.FlexibleSpace();
		EditorGUILayout.EndHorizontal();

		EndPage();
	}

	void VersionChanges ()
	{
		StartPage( versionChanges );

		EditorGUILayout.LabelField( "  Thank you for downloading the most recent version of the Ultimate Joystick. There has been some major changes that could affect any existing reference of the Ultimate Joystick. If you are experiencing any errors, please completely remove the Ultimate Joystick from your project and re-import it. As always, if you run into any issues with the Ultimate Joystick, please contact us at:", paragraphStyle );

		GUILayout.Space( paragraphSpace );
		EditorGUILayout.SelectableLabel( "tankandhealerstudio@outlook.com", itemHeaderStyle, GUILayout.Height( 15 ) );
		GUILayout.Space( sectionSpace );

		EditorGUILayout.LabelField( "GENERAL CHANGES", sectionHeaderStyle );
		EditorGUILayout.LabelField( "  • We have updated the Folder Structure yet again to help conform to the new way of doing things for the Unity Asset Store All files have been moved into a base folder named 'Ultimate Joystick'.", paragraphStyle );
		EditorGUILayout.LabelField( "  • The way to create an Ultimate Joystick has been changed because of the new folder structure. Please see the How To section to learn more about how to add an Ultimate Joystick to your scene now.", paragraphStyle );
		EditorGUILayout.LabelField( "  • The Ultimate Joystick Editor has been simplified to help make it easier to use and understand.", paragraphStyle );
		EditorGUILayout.LabelField( "  • The Ultimate Joystick Documentation Window has been slightly changed to be easier to use.", paragraphStyle );
		EditorGUILayout.LabelField( "  • The Ultimate Joystick Documentation Window now has a Settings page where you can change the font size to your preference.", paragraphStyle );

		GUILayout.Space( 10 );

		EditorGUILayout.LabelField( "NEW FUNCTIONS", sectionHeaderStyle );
		EditorGUILayout.LabelField( "  Some new functions have been added to help reference the Ultimate Joystick more efficiently. For information on what each new function does, please refer to the Documentation section of this help window.", paragraphStyle );
		EditorGUILayout.LabelField( "  • float GetHorizontalAxisRaw()", paragraphStyle );
		EditorGUILayout.LabelField( "  • float GetVerticalAxisRaw()", paragraphStyle );
		EditorGUILayout.LabelField( "  • DisableJoystick()", paragraphStyle );
		EditorGUILayout.LabelField( "  • EnableJoystick()", paragraphStyle );

		GUILayout.Space( 10 );
		
		EditorGUILayout.LabelField( "REMOVED FUNCTIONS", sectionHeaderStyle );
		EditorGUILayout.LabelField( "  • Vector2 GetPosition()", paragraphStyle );
		
		GUILayout.Space( 10 );
		
		EditorGUILayout.LabelField( "RENAMED FUNCTIONS", sectionHeaderStyle );
		EditorGUILayout.LabelField( "  • GetJoystick has been renamed to GetUltimateJoystick to help show that it returns a Ultimate Joystick component.", paragraphStyle );
		
		GUILayout.Space( 15 );

		EditorGUILayout.BeginHorizontal();
		GUILayout.FlexibleSpace();
		if( GUILayout.Button( "Got it!", buttonSize ) )
			NavigateBack();
		
		var rect = GUILayoutUtility.GetLastRect();
		EditorGUIUtility.AddCursorRect( rect, MouseCursor.Link );

		GUILayout.FlexibleSpace();
		EditorGUILayout.EndHorizontal();
		
		EndPage();
	}

	void WindowSettings ()
	{
		StartPage( settings );

		EditorGUI.BeginChangeCheck();
		fontSize = ( FontSize )EditorGUILayout.EnumPopup( "Font Size", fontSize );
		if( EditorGUI.EndChangeCheck() )
		{
			switch( fontSize )
			{
				case FontSize.Small:
				default:
				{
					GUI.skin.textArea.fontSize = 11;
					paragraphStyle.fontSize = 11;
					itemHeaderStyle.fontSize = 11;
					sectionHeaderStyle.fontSize = 14;
				}
				break;
				case FontSize.Medium:
				{
					GUI.skin.textArea.fontSize = 12;
					paragraphStyle.fontSize = 12;
					itemHeaderStyle.fontSize = 12;
					sectionHeaderStyle.fontSize = 16;
				}
				break;
				case FontSize.Large:
				{
					GUI.skin.textArea.fontSize = 14;
					paragraphStyle.fontSize = 14;
					itemHeaderStyle.fontSize = 14;
					sectionHeaderStyle.fontSize = 18;
				}
				break;
			}
		}

		GUILayout.Space( 20 );
		
		EditorGUILayout.LabelField( "Example Text", sectionHeaderStyle );
		GUILayout.Space( paragraphSpace );
		EditorGUILayout.LabelField( "Example Text", itemHeaderStyle );
		GUILayout.Space( paragraphSpace );
		EditorGUILayout.LabelField( "This is an example paragraph to see the size of the text after modification.", paragraphStyle );

		EndPage();
	}

	void StartPage ( PageInformation pageInfo )
	{
		pageInfo.scrollPosition = EditorGUILayout.BeginScrollView( pageInfo.scrollPosition, false, false, docSize );
		GUILayout.Space( 15 );
	}

	void EndPage ()
	{
		EditorGUILayout.EndScrollView();
	}

	void ShowDocumentation ( DocumentationInfo info )
	{
		GUILayout.Space( paragraphSpace );

		EditorGUILayout.LabelField( info.functionName, itemHeaderStyle );
		var rect = GUILayoutUtility.GetLastRect();
		EditorGUIUtility.AddCursorRect( rect, MouseCursor.Link );
		if( Event.current.type == EventType.MouseDown && rect.Contains( Event.current.mousePosition ) && ( info.showMore.faded == 0.0f || info.showMore.faded == 1.0f ) )
		{
			info.showMore.target = !info.showMore.target;
		}

		if( EditorGUILayout.BeginFadeGroup( info.showMore.faded ) )
		{
			if( info.parameter != null )
			{
				for( int i = 0; i < info.parameter.Length; i++ )
					EditorGUILayout.LabelField( Indent + "<i>Parameter:</i> " + info.parameter[ i ], paragraphStyle );
			}
			if( info.returnType != string.Empty )
				EditorGUILayout.LabelField( Indent + "<i>Return type:</i> " + info.returnType, paragraphStyle );

			EditorGUILayout.LabelField( Indent + "<i>Description:</i> " + info.description, paragraphStyle );

			if( info.codeExample != string.Empty )
				EditorGUILayout.TextArea( info.codeExample, GUI.skin.textArea );

			GUILayout.Space( paragraphSpace );
		}
		EditorGUILayout.EndFadeGroup();
	}

	string Indent
	{
		get
		{
			return "    ";
		}
	}

	[InitializeOnLoad]
	class UltimateJoystickInitialLoad
	{
		static UltimateJoystickInitialLoad ()
		{
			// If the user has a older version of UJ that used the bool for startup...
			if( EditorPrefs.HasKey( "UltimateJoystickStartup" ) && !EditorPrefs.HasKey( "UltimateJoystickVersion" ) )
			{
				// Set the new pref to 0 so that the pref will exist and the version changes will be shown.
				EditorPrefs.SetInt( "UltimateJoystickVersion", 0 );
			}

			// If this is the first time that the user has downloaded the Ultimate Joystick...
			if( !EditorPrefs.HasKey( "UltimateJoystickVersion" ) )
			{
				// Navigate to the Thank You page.
				NavigateForward( thankYou );

				// Set the version to current so they won't see these version changes.
				EditorPrefs.SetInt( "UltimateJoystickVersion", importantChanges );

				EditorApplication.update += WaitForCompile;
			}
			else if( EditorPrefs.GetInt( "UltimateJoystickVersion" ) < importantChanges )
			{
				// Navigate to the Version Changes page.
				NavigateForward( versionChanges );

				// Set the version to current so they won't see this page again.
				EditorPrefs.SetInt( "UltimateJoystickVersion", importantChanges );

				EditorApplication.update += WaitForCompile;
			}
		}

		static void WaitForCompile ()
		{
			if( EditorApplication.isCompiling )
				return;

			EditorApplication.update -= WaitForCompile;

			InitializeWindow();
		}
	}
}