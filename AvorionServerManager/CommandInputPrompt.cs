using System.Collections.Generic;
using System.Windows.Forms;
using AvorionServerManager.Commands;
namespace AvorionServerManager
{
    class CommandInputPrompt
    {
        public static DialogResult ShowInputDialog(string title,List<AvorionServerCommandParameterDefinition> parameterDefinitions,ref List<AvorionServerCommandParameter> parameters)
        {
            System.Drawing.Size size = new System.Drawing.Size(500, 60*(parameterDefinitions.Count+1));
            Form inputBox = new Form();

            inputBox.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            inputBox.ClientSize = size;
            inputBox.Text = title;
            int counter = 0;
            List<TextBox> textBoxes = new List<TextBox>();
            foreach (AvorionServerCommandParameterDefinition currentParameterDefinition in parameterDefinitions)
            {
                Label tmpLabel = new Label();
                tmpLabel.Text = currentParameterDefinition.DisplayName;
                tmpLabel.Location = new System.Drawing.Point(5, 5 + (60 * counter));

                TextBox tmpTextBox = new TextBox();
                tmpTextBox.Size = new System.Drawing.Size(size.Width - 10, 23);
                tmpTextBox.Location = new System.Drawing.Point(5, 30+(60*counter));
                inputBox.Controls.Add(tmpLabel);
                inputBox.Controls.Add(tmpTextBox);
                textBoxes.Add(tmpTextBox);
                AvorionServerCommandParameter tmpParameter = new AvorionServerCommandParameter();
                tmpParameter.Prefix = currentParameterDefinition.Prefix;
                parameters.Add(tmpParameter);
                counter++;
            }

            Button okButton = new Button();
            okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            okButton.Name = "okButton";
            okButton.Size = new System.Drawing.Size(75, 23);
            okButton.Text = "&Send";
            okButton.Location = new System.Drawing.Point(size.Width - 80 - 80, size.Height - 40);
            inputBox.Controls.Add(okButton);

            Button cancelButton = new Button();
            cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            cancelButton.Name = "cancelButton";
            cancelButton.Size = new System.Drawing.Size(75, 23);
            cancelButton.Text = "&Cancel";
            cancelButton.Location = new System.Drawing.Point(size.Width - 80, size.Height-40);
            inputBox.Controls.Add(cancelButton);

            inputBox.AcceptButton = okButton;
            inputBox.CancelButton = cancelButton;

            DialogResult result = inputBox.ShowDialog();
            int tmpParameterIndex = 0;
            foreach(TextBox currentTextBox in textBoxes)
            {
                parameters[tmpParameterIndex].Content = currentTextBox.Text;
                tmpParameterIndex++;
            }
            return result;
        }
    }
}
