public enum InstructionType
{
    EnterCrimeScene,
    EnterForensicsLab,
    EnterCity
}

public static class Instructions
{
    public static string Get(InstructionType type)
    {
        switch (type)
        {
            case InstructionType.EnterCrimeScene:
                return "Press <b>E</b> to open the door and enter the <color=#FF5555>Crime Scene</color>.";

            case InstructionType.EnterForensicsLab:
                return "Press <b>E</b> to open the door and enter the <color=#4FC3F7>Forensics Lab</color>.";

            case InstructionType.EnterCity:
                return "Press <b>E</b> to open the door and go back to the <color=#A5D6A7>City</color>.";

            default:
                return "";
        }
    }
}