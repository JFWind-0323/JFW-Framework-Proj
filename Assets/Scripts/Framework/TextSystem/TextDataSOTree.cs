using Sirenix.OdinInspector;
using TextSystem;
using UnityEngine;
using Utilities;


namespace Framework.TextSystem
{
    [CreateAssetMenu(fileName = "TextDataSOTree", menuName = "SO/TextData/Tree", order = 0)]
    public class TextDataSOTree : TextDataSOBase<LineTree>
    {
        public string[] characters;
        public string[] positions;
        private Tree<LineTree> dialogueTree;

        protected override void SplitLine(string content)
        {
            var split = content.Split("\n");
            for (var i = 1; i < split.Length; i++)
            {
                if (string.IsNullOrEmpty(split[i]))
                {
                }
                else
                {
                    var line = split[i].Trim().Split(",");
                    lines.Add(new LineTree(line, characters, positions));
                }
            }
            dialogueTree = new Tree<LineTree>(lines[0]);
            ProcessLine(lines[0]);
        }

        [Button("Print")]
        public void Print()
        {
            foreach (var lineNode in dialogueTree.GetDFSIterative())
            {
                Debug.Log(lineNode.Value.character + ": " + lineNode.Value.text);
            }
        }

        private void ProcessLine(LineTree rootLine)
        {
            if (rootLine == null)
            {
                Debug.LogWarning("Line not found");
                return;
            }
            
            TreeNode<LineTree> parentNode = dialogueTree.FindNode(lineTree => lineTree.id == rootLine.id);
            LineTree currentLine = lines[rootLine.id+1];
            LineTree nextLine = null;
            for (var i = rootLine.id + 1; i < lines.Count; i++)
            {
                if (currentLine == null)
                    break;
                if (currentLine.type == LineType.Default)
                {
                    parentNode = AddLineNode(parentNode.Value, currentLine);
                    nextLine = GetLogicNextLine(currentLine);
                }
                //记得处理第零条就是问题的情况，即第一条开始就是选项
                else if (currentLine.type == LineType.Question)
                {
                    parentNode = AddLineNode(parentNode.Value, currentLine);
                    nextLine = GetLogicNextLine(currentLine);
                    while (nextLine.type == LineType.Option)
                    {
                        AddLineNode(parentNode.Value, nextLine);
                        ProcessLine(nextLine);
                        nextLine = GetPhysicalNextLine(nextLine);
                    }
                }
                else if (currentLine.type == LineType.Option)
                {
                    nextLine = GetLogicNextLine(currentLine);
                }
                else if (currentLine.type == LineType.End)
                {
                    AddLineNode(parentNode.Value, currentLine);
                    break;
                }

                currentLine = nextLine;
            }
        }

        private TreeNode<LineTree> AddLineNode(LineTree parentLine, LineTree childLine)
        {
            TreeNode<LineTree> node = dialogueTree.FindNode(lineTree => lineTree.id == parentLine.id);

            if (node == null)
            {
                Debug.LogError("Parent line not found in tree!");
                return null;
            }
            Debug.Log(childLine.character + ": " + childLine.text);

            return dialogueTree.AddNode(node, childLine);
        }

        private LineTree GetLogicNextLine(LineTree currentLine)
        {
            if (currentLine.next == -1)
            {
                return null;
            }
            else
            {
                return lines[currentLine.next];
            }
        }

        private LineTree GetPhysicalNextLine(LineTree currentLine)
        {
            if (currentLine.id == lines.Count - 1)
            {
                Debug.Log("End of dialogue data!");
                return null;
            }
            return lines[currentLine.id + 1];
        }
    }
}