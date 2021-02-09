﻿// ReSharper disable StringLiteralTypo
// ReSharper disable IdentifierTypo
// ReSharper disable InconsistentNaming
// ReSharper disable UnusedType.Global
namespace InControl.UnityDeviceProfiles
{
    // @cond nodoc
    [Preserve]
    [UnityInputDeviceProfile]
    public class AndroidTVAmazonUnityProfile : InputDeviceProfile
    {
        public override void Define()
        {
            base.Define();

            DeviceName = "Android TV Controller";
            DeviceNotes = "Android TV Controller on Amazon Fire TV";

            DeviceClass = InputDeviceClass.Controller;

            IncludePlatforms = new[]
            {
                "Amazon AFT"
            };

            Matchers = new[] { new InputDeviceMatcher { NameLiteral = "Gamepad" } };

            ButtonMappings = new[]
            {
                new InputControlMapping
                {
                    Name = "A",
                    Target = InputControlType.Action1,
                    Source = Button( 0 )
                },
                new InputControlMapping
                {
                    Name = "B",
                    Target = InputControlType.Action2,
                    Source = Button( 1 )
                },
                new InputControlMapping
                {
                    Name = "X",
                    Target = InputControlType.Action3,
                    Source = Button( 2 )
                },
                new InputControlMapping
                {
                    Name = "Y",
                    Target = InputControlType.Action4,
                    Source = Button( 3 )
                },
                new InputControlMapping
                {
                    Name = "Left Bumper",
                    Target = InputControlType.LeftBumper,
                    Source = Button( 4 )
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
                    Source = Button( 8 )
                },
                new InputControlMapping
                {
                    Name = "Right Stick Button",
                    Target = InputControlType.RightStickButton,
                    Source = Button( 9 )
                },
                new InputControlMapping
                {
                    Name = "Back",
                    Target = InputControlType.Back,
                    Source = EscapeKey
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
                DPadUpMapping( 5 ),
                DPadDownMapping( 5 ),

                new InputControlMapping
                {
                    Name = "Left Trigger",
                    Target = InputControlType.LeftTrigger,
                    Source = Analog( 12 ),
                },
                new InputControlMapping
                {
                    Name = "Right Trigger",
                    Target = InputControlType.RightTrigger,
                    Source = Analog( 11 ),
                },
            };
        }
    }

    // @endcond
}
