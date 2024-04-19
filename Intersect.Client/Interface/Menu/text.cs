using System.IO;
using Intersect.Client.Core;
using Intersect.Client.Framework.File_Management;
using Intersect.Client.Framework.Graphics;
using Intersect.Client.Framework.Gwen.Control;
using Intersect.Client.Framework.Gwen.Control.EventArguments;
using Intersect.Client.Localization;
using Intersect.Configuration;
using Newtonsoft.Json;

namespace Intersect.Client.Interface.Menu
{

    public partial class textWindow : IMainMenuWindow
    {

        private Button mbackeBtn;

        //Content
        private ScrollControl mtextContent;

        //Parent
        private Label mtextHeader;

        //Controls
        private ImagePanel mtextWindow;

        private MainMenu mtextMenu;

        private RichLabel mtextLabel;

        //Init
        public textWindow(Canvas parent, MainMenu mainMenu)
        {
            //Assign References
            mtextMenu = mainMenu;

            //Main Menu Window
            mtextWindow = new ImagePanel(parent, "textWindow");

            //Menu Header
            mtextHeader = new Label(mtextWindow, "textHeader");
            mtextHeader.SetText(Strings.Text.title);

            mtextContent = new ScrollControl(mtextWindow, "textScrollview");
            mtextContent.EnableScroll(false, true);

            mtextLabel = new RichLabel(mtextContent, "textLabel");

            //Back Button
            mbackeBtn = new Button(mtextWindow, "BackButton");
            mbackeBtn.SetText(Strings.Text.back);
            mbackeBtn.Clicked += backeBtn_Clicked;

            mtextWindow.LoadJsonUi(GameContentManager.UI.Menu, Graphics.Renderer.GetResolutionString());
        }

        private void backeBtn_Clicked(Base sender, ClickedEventArgs arguments)
        {
            Hide();
            mtextMenu.Show();
        }

        //Methods
        public void Update()
        {
        }

        public void Hide()
        {
            mtextWindow.IsHidden = true;
        }

        public void Show()
        {
            mtextWindow.IsHidden = false;
            mtextLabel.ClearText();
            var texty = new Textparser();
            var textyFile = Path.Combine(ClientConfiguration.ResourcesDirectory, "text.json");
            if (File.Exists(textyFile))
            {
                texty = JsonConvert.DeserializeObject<Textparser>(File.ReadAllText(textyFile));
            }
            else
            {
                var line = new Textparser.TextLine();
                line.Text = "Insert your credits here!";
                line.Alignment = "center";
                line.Size = 12;
                line.Clr = Intersect.Color.White;
                line.Font = "sourcesansproblack";
                texty.Lines.Add(line);
            }

            File.WriteAllText(textyFile, JsonConvert.SerializeObject(texty, Formatting.Indented));

            foreach (var line in texty.Lines)
            {
                if (line.Text.Trim().Length == 0)
                {
                    mtextLabel.AddLineBreak();
                }
                else
                {
                    mtextLabel.AddText(
                        line.Text, new Color(line.Clr.A, line.Clr.R, line.Clr.G, line.Clr.B), line.GetAlignment(),
                        GameContentManager.Current.GetFont(line.Font, line.Size)
                    );

                    mtextLabel.AddLineBreak();
                }
            }

            mtextLabel.SizeToChildren(false, true);
        }

    }

}
