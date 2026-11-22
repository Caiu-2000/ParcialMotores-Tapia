public class GoMenuAutomatic : ScapeOrBackReader
{
    protected override void GoBackPressed()
    {
        MainMenu.instance.GoMenu();
    }
}
