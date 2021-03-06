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

internal class WithStatement : StatementNode
{
	public WithStatement(IToken o) : base(o)
	{
	}

	internal override void GenStatementCode(CompileContext compileContext, bool bGenDebug)
	{
		compileContext.gen.Emit(OpCodes.Ldarg, 0);
		((ExpressionNode) base.Children[0]).GenCode(compileContext);
		compileContext.gen.EmitCallV( CompileContext.mi_JSScope_CreateWithScope);
		compileContext.gen.Emit(OpCodes.Starg, 0);
		Label InternalTry = compileContext.gen.BeginExceptionBlock();
		base.TryCatchLevels++;
		((StatementNode) base.Children[1]).GenCode(compileContext);
		base.TryCatchLevels--;
		compileContext.gen.BeginFinallyBlock();
		compileContext.gen.Emit(OpCodes.Ldarg_0);
		compileContext.gen.EmitCallV( CompileContext.mi_JSContext_Parent);
		compileContext.gen.Emit(OpCodes.Starg, 0);
		compileContext.gen.EndExceptionBlock();
	}
}

