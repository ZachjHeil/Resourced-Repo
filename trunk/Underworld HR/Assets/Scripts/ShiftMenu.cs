using UnityEngine;
using UnityEngine.UIElements;

public class ShiftMenu : VisualElement {
    private VisualElement leftArrow, rightArrow;
    private Label label;

    private int index;
    private string[] options;

    public int Index {
	get { return index; }
    }
    
    
    public ShiftMenu(string name, string[] optionList) : base() {
	
	leftArrow = new Button();
	rightArrow = new Button();
	label = new Label();

	this.name = name;
	options = optionList;

	//this.name = namePrefix;
	//this.name = namePrefix+"-base";
	// leftArrow.name = namePrefix+"-leftarrow";
	// rightArrow.name = namePrefix+"-rightarrow";

	label.text = "Test!";

	this.AddToClassList("shiftmenu");
	leftArrow.AddToClassList("shiftmenu-leftarrow");
	rightArrow.AddToClassList("shiftmenu-rightarrow");
	label.AddToClassList("shiftmenu-label");

	this.Add(leftArrow);
	this.Add(label);
	this.Add(rightArrow);

	leftArrow.RegisterCallback<ClickEvent>(ev => OnShiftLeft() );
	rightArrow.RegisterCallback<ClickEvent>(ev => OnShiftRight() );

	label.text = options[index];
    }

    public void SetIndex(int ind){
	index = ind;
	label.text = options[index];
    }

    private void OnShiftLeft(){
	int oldIndex = index;
        //index = (options.Length + index - 1)%options.Length;
	SetIndex((options.Length + index - 1)%options.Length);
	//label.text = options[index];

	// ChangeEvent<int> ev = new ChangeEvent<int>();
	// ev.previousValue = oldIndex;
	// ev.newValue = index;

	var e = ChangeEvent<int>.GetPooled(oldIndex,index);
	e.target = this;
	this.SendEvent(e);

    }

    private void OnShiftRight(){
	int oldIndex = index;
	SetIndex((index + 1)%options.Length);
        // index = (index + 1)%options.Length;
	// label.text = options[index];

	var e = ChangeEvent<int>.GetPooled(oldIndex,index);
	e.target = this;
	this.SendEvent(e);
    }
}
