// ReSharper disable StringLiteralTypo
// ReSharper disable IdentifierTypo
// ReSharper disable InconsistentNaming
// ReSharper disable UnusedType.Global
namespace InControl.UnityDeviceProfiles
{
    // @cond nodoc
    [Preserve]
    [UnityInputDeviceProfile]
    public class NexusPlayerWindowsUnityProfile : InputDeviceProfile
    {
        // No trigger support, sadly. They're probably out of the
        // element range Unity supports.
        //
        public override void Define()
        {
            base.Define();

            DeviceName = "Nexus Player Controller";
            DeviceNotes = "Nexus Player Controller on Windows";

            DeviceClass = InputDeviceClass.Controller;

            IncludePlatforms = new[]
            {
                "Windows",
            };

            Matchers = new[] { new InputDeviceMatcher { NameLiteral = "GamePad" } };

            ButtonMappings = new[]
            {
                new InputControlMapping
                {
                    Name = "A",
                    Target = InputControlType.Action1,
                    Source = Button( 10 )
                },
                new InputControlMapping
                {
                    Name = "B",
                    Target = InputControlType.Action2,
                    Source = Button( 9 )
                },
                new InputControlMapping
                {
                    Name = "X",
                    Target = InputControlType.Action3,
                    Source = Button( 8 )
                },
                new InputControlMapping
                {
                    Name = "Y",
                    Target = InputControlType.Action4,
                    Source = Button( 7 )
                },
                new InputControlMapping
                {
                    Name = "Left Bumper",
                    Target = InputControlType.LeftBumper,
                    Source = Button( 6 )
                },
                new InputControlMapping
                {
                    Name = "Right Bumper",
                    Target = InputControlType.RightBumper,
                    Source = Button( 5 )
                },
                new InputControlMapping
                {
                    Name = "Left Stick Button",
                    Target = InputControlType.LeftStickButton,
                    Source = Button( 4 )
                },
                new InputControlMapping
                {
                    Name = "Right Stick Button",
                    Target = InputControlType.RightStickButton,
                    Source = Button( 3 )
                },
                new InputControlMapping
                {
                    Name = "Back",
                    Target = InputControlType.Select,
                    Source = Button( 1 )
                },
                new InputControlMapping
                {
                    Name = "Start",
                    Target = InputControlType.Start,
                    Source = Button( 0 )
                },
                new InputControlMapping
                {
                    Name = "System",
                    Target = InputControlType.System,
                    Source = Button( 2 )
                }
            };

            AnalogMappings = new[]
            {
                LeftStickLeftMapping( 0 ),
                LeftStickRightMapping( 0 ),
                LeftStickUpMapping( 1 ),
                LeftStickDownMapping( 1 ),

                RightStickLeftMapping( 2 ),
                RightStickRightMapping( 2 ),
                RightStickUpMapping( 3 ),
                RightStickDownMapping( 3 ),

                DPadLeftMapping( 4 ),
                DPadRightMapping( 4 ),
                DPadUpMapping2( 5 ),
                DPadDownMapping2( 5 ),

				//				new InputControlMapping {
				//					Handle = "Left Trigger",
				//					Target = InputControlType.LeftTrigger,
				//					Source = Analog( 9 ),
				//					SourceRange = InputRangeType.ZeroToOne,
				//					TargetRange = InputRangeType.ZeroToOne,
				//				},
				//				new InputControlMapping {
				//					Handle = "Right Trigger",
				//					Target = InputControlType.RightTrigger,
				//					Source = Analog( 9 ),
				//					SourceRange = InputRangeType.ZeroToMinusOne,
				//					TargetRange = InputRangeType.ZeroToOne,
				//				}
			};
        }
    }

    // @endcond
}
