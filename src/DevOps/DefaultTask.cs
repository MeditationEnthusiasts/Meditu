﻿//
// Meditation Logger.
// Copyright (C) 2017  Seth Hendrick.
//
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
//
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with this program.  If not, see <http://www.gnu.org/licenses/>.
//

using System.Runtime.ExceptionServices;
using Cake.Common.Diagnostics;
using Cake.Frosting;

namespace DevOps
{
    public class DefaultTask : FrostingTask<MeditationLogContext>
    {
        // ---------------- Functions ----------------

        public override void OnError( Exception exception, MeditationLogContext context )
        {
            // We want the stack trace to print out when all is said and done.
            // The way to do this is to set the verbosity to the maximum,
            // and then re-throw the exception.  Use the weird DispatchInfo
            // class so we don't get a new stack trace.
            // We need to re-throw the exception, or cake will exit with a zero exit code.
            context.DiagnosticVerbosity();
            ExceptionDispatchInfo.Capture( exception ).Throw();
        }
    }
}