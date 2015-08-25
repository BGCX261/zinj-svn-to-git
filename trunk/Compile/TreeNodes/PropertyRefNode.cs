﻿/* 
 * Copyright 2010 Thomas Horne
 * 
 * This file is part of the Zinj EcmaScript Engine.
 * 
 * Zinj is free software: you can redistribute it and/or modify it under 
 * the terms of the GNU General Public License as published by the Free 
 * Software Foundation, either version 3 of the License, or (at your 
 * option) any later version.
 * 
 * Zinj is distributed in the hope that it will be useful, but WITHOUT 
 * ANY WARRANTY; without even the implied warranty of MERCHANTABILITY 
 * or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public 
 * License for more details.
 * 
 * You should have received a copy of the GNU General Public License 
 * along with Zinj. If not, see http://www.gnu.org/licenses/.
 */

using Zinj;
using Antlr.Runtime;
using Antlr.Runtime.Tree;
using System;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;

internal class PropertyRefNode : BaseRefNode
{
	public PropertyRefNode(IToken t) : base(t)
	{
	}

	internal override void ConstructionComplete()
	{
		this.Value = CompileContext.EscapeIdentifier(((CommonTree) base.Children[0]).Text);
		base.Children.RemoveAt(0);
		base.LeftTree = (ExpressionNode) base.Children[0];
		base.ConstructionComplete();
	}

	protected override void LdRefValue(CompileContext compileContext)
	{
		compileContext.gen.Emit(OpCodes.Ldstr, this.Value);
	}

	public string Value { get; private set; }
}

