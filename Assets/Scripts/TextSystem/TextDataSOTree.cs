using UnityEngine;
using Utilities;

namespace TextSystem
{
    [CreateAssetMenu(fileName = "TextDataSOTree", menuName = "SO/TextData/Tree", order = 0)]
    public class TextDataSOTree : TextDataSOBase<LineTree>
    {
        public string[] characters;
        public string[] positions;
        private Tree<LineTree> dialogueTree;
        private LineTree currentLine;

        void OnEnable()
        {
            ProcessTextData();
            currentLine = dialogueTree.root.Value;
        }

        #region 外部获取数据

        public void UpdateCurrentLine(LineTree nextLine)
        {
            currentLine = nextLine;
        }

        public LineTree GetCurrentLine(out int childrenCount)
        {
            var currentNode = dialogueTree.FindNode(currentLine);
            childrenCount = dialogueTree.GetChildren(currentNode).Count;
            return currentLine;
        }

        public LineTree GetNextLine(int childIndex = 0)
        {
            var currentNode = dialogueTree.FindNode(currentLine);
            var nextNode = dialogueTree.GetChildNode(currentNode, childIndex);
            return nextNode.Value;
        }

        #endregion


        #region 数据调试与处理
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

        protected override void Print()
        {
            foreach (var lineNode in dialogueTree.GetDFSIterative())
            {
                Debug.Log(lineNode.Value.character + ": " + lineNode.Value.content);
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
            LineTree nextLine = GetLogicNextLine(rootLine);
            for (var i = nextLine.id; i < lines.Count; i++)
            {
                var currentLine = nextLine;
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

                    return;
                }
                else if (currentLine.type == LineType.Option)
                {
                    nextLine = GetLogicNextLine(currentLine);
                    if (nextLine.type == LineType.End)
                    {
                        AddLineNode(parentNode.Value, nextLine);
                        return;
                    }
                }
                else if (currentLine.type == LineType.End)
                {
                    AddLineNode(parentNode.Value, currentLine);
                    break;
                }
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

            return dialogueTree.AddNode(node, childLine);
        }

        private LineTree GetLogicNextLine(LineTree currentLine)
        {
            if (currentLine.next == -1)
            {
                Debug.Log("End of dialogue data!");
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

        #endregion
    }
}