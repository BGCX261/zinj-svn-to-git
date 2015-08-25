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
using System;
using System.Reflection.Emit;

internal class CompoundAddNode : CompoundAssignmentNode
{
	public CompoundAddNode(IToken o) : base(o)
	{
	}

	protected override void ApplyOp(CompileContext compileContext)
	{
		compileContext.gen.EmitCallV( CompileContext.op_JSValue_Addition);
	}

	protected override void GenRightVal(CompileContext compileContext)
	{
		((ExpressionNode) base.Children[1]).GenCode(compileContext);
	}

	public override JSType TreeType
	{
		get
		{
			return JSType.Number;
		}
	}
}

