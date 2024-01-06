using Godot;
using System;

public partial class UpgradeButton : TextureButton
{
	public Upgrade upgradeIdentity;
	PackedScene toolTip;
	Control tooltipInst;
	bool mouseOnButton = false;

	public override void _Ready()
	{
		this.Pressed += OnPressed;
		this.MouseEntered += OnMouseEntered;
		this.MouseExited += OnMouseExit;
		toolTip = GD.Load<PackedScene>("res://Scenes/tooltip.tscn");
	}

	public void LoadUpgradeIdentity(Upgrade ug)
	{
		upgradeIdentity = ug;
		//This should work but it doesn't, angry
		TextureNormal = ug.upgradeIcon;
	}
	
	public void OnPressed()
	{
		if (upgradeIdentity.isAvailable() && Stats.devotion > upgradeIdentity.upgradePrice)
		{
			GetNode<UpgradeManager>("/root/root/UpgradeManager").GainUpgrade(upgradeIdentity, this);
			QueueFree();
		}
	}
	
	public void OnMouseEntered()
	{
		mouseOnButton = true;
		SpawnTooltip();
	}
	
	async void SpawnTooltip()
	{
		await ToSignal(GetTree().CreateTimer(1), "timeout");
		if (!mouseOnButton)
		{
			return;
		}
		GD.Print("Mouse has begun hovering over button!");
		Control tooltipInstance = (Control)toolTip.Instantiate();
		this.AddChild(tooltipInstance);
		tooltipInstance.GlobalPosition = GetViewport().GetMousePosition();
		tooltipInst = tooltipInstance;
		tooltipInstance.GetChild<Label>(1).Text = upgradeIdentity.upgradeName;
		tooltipInstance.GetChild<Label>(2).Text = Stats.NumAbbreviate(upgradeIdentity.upgradePrice) + " Dv";
		tooltipInstance.GetChild<Label>(3).Text = upgradeIdentity.upgradeText;
		tooltipInstance.GetChild<Label>(4).Text = upgradeIdentity.upgradeTip;
		
	}
	
	public void OnMouseExit()
	{
		if (tooltipInst != null)
		{
			tooltipInst.QueueFree();
			tooltipInst = null;
		}
		mouseOnButton = false;
	}
}
