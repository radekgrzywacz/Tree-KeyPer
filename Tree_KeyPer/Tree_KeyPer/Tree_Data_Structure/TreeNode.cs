using System.Collections;

namespace Tree_KeyPer.Tree_Data_Structure;

public class TreeNode<T> : IEnumerable
{
    public T Data { get; set; }
    public HashSet<TreeNode<T>> Children { get; set; } = new HashSet<TreeNode<T>>();
    public HashSet<TreeNode<T>> Parents { get; set; } = new HashSet<TreeNode<T>>();

    public TreeNode(T data)
    {
        Data = data;
    }
    
    
    
    
    
    // TODO: Something with this idk ------     Huge impro ???????
    public IEnumerator<TreeNode<T>> GetEnumerator()
    {
        yield return this; // Return the current node

        foreach (var child in Children)
        {
            foreach (var item in child)
            {
                yield return item; // Recursively return children
            }
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
    
}