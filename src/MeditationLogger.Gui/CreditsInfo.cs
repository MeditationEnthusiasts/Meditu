//
// Meditu - A way to track Meditation Sessions.
// Copyright (C) 2017-2021 Seth Hendrick.
// 
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU Affero General Public License as published
// by the Free Software Foundation, either version 3 of the License, or
// any later version.
// 
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU Affero General Public License for more details.
// 
// You should have received a copy of the GNU Affero General Public License
// along with this program.  If not, see <https://www.gnu.org/licenses/>.
//

using System.Collections.Generic;

namespace Meditu.Gui
{
    public struct CreditsInfo
    {
        // ---------------- Constructor --------------

        static CreditsInfo()
        {
            List<CreditsInfo> info = new List<CreditsInfo>();
            AllCredits = info.AsReadOnly();

            {
                CreditsInfo binaryTheme = new CreditsInfo()
                {
                    License = "cc3.0",
                    LicenseUrl = "https://creativecommons.org/licenses/by/3.0/",
                    Purpose = "For site Template",
                    Title = "Binary Theme",
                    Url = "http://binarytheme.com/",
                    CodeUrl = "http://binarytheme.com/responsive-advance-admin-template/",
                    LicenseText =
@"
<h3><em>License</em></h3>
<p>THE WORK (AS DEFINED BELOW) IS PROVIDED UNDER THE TERMS
OF THIS CREATIVE COMMONS PUBLIC LICENSE (""CCPL"" OR
""LICENSE""). THE WORK IS PROTECTED BY COPYRIGHT AND/OR OTHER
APPLICABLE LAW. ANY USE OF THE WORK OTHER THAN AS
AUTHORIZED UNDER THIS LICENSE OR COPYRIGHT LAW IS
PROHIBITED.</p>
<p>BY EXERCISING ANY RIGHTS TO THE WORK PROVIDED HERE, YOU
ACCEPT AND AGREE TO BE BOUND BY THE TERMS OF THIS LICENSE.
TO THE EXTENT THIS LICENSE MAY BE CONSIDERED TO BE A
CONTRACT, THE LICENSOR GRANTS YOU THE RIGHTS CONTAINED HERE
IN CONSIDERATION OF YOUR ACCEPTANCE OF SUCH TERMS AND
CONDITIONS.</p>
<p><strong>1. Definitions</strong></p>
<ol type=""a"">
<li><strong>""Adaptation""</strong> means a work based upon
the Work, or upon the Work and other pre-existing works,
such as a translation, adaptation, derivative work,
arrangement of music or other alterations of a literary
or artistic work, or phonogram or performance and
includes cinematographic adaptations or any other form in
which the Work may be recast, transformed, or adapted
including in any form recognizably derived from the
original, except that a work that constitutes a
Collection will not be considered an Adaptation for the
purpose of this License. For the avoidance of doubt,
where the Work is a musical work, performance or
phonogram, the synchronization of the Work in
timed-relation with a moving image (""synching"") will be
considered an Adaptation for the purpose of this
License.</li>
<li><strong>""Collection""</strong> means a collection of
literary or artistic works, such as encyclopedias and
anthologies, or performances, phonograms or broadcasts,
or other works or subject matter other than works listed
in Section 1(f) below, which, by reason of the selection
and arrangement of their contents, constitute
intellectual creations, in which the Work is included in
its entirety in unmodified form along with one or more
other contributions, each constituting separate and
independent works in themselves, which together are
assembled into a collective whole. A work that
constitutes a Collection will not be considered an
Adaptation (as defined above) for the purposes of this
License.</li>
<li><strong>""Distribute""</strong> means to make available
to the public the original and copies of the Work or
Adaptation, as appropriate, through sale or other
transfer of ownership.</li>
<li><strong>""Licensor""</strong> means the individual,
individuals, entity or entities that offer(s) the Work
under the terms of this License.</li>
<li><strong>""Original Author""</strong> means, in the case
of a literary or artistic work, the individual,
individuals, entity or entities who created the Work or
if no individual or entity can be identified, the
publisher; and in addition (i) in the case of a
performance the actors, singers, musicians, dancers, and
other persons who act, sing, deliver, declaim, play in,
interpret or otherwise perform literary or artistic works
or expressions of folklore; (ii) in the case of a
phonogram the producer being the person or legal entity
who first fixes the sounds of a performance or other
sounds; and, (iii) in the case of broadcasts, the
organization that transmits the broadcast.</li>
<li><strong>""Work""</strong> means the literary and/or
artistic work offered under the terms of this License
including without limitation any production in the
literary, scientific and artistic domain, whatever may be
the mode or form of its expression including digital
form, such as a book, pamphlet and other writing; a
lecture, address, sermon or other work of the same
nature; a dramatic or dramatico-musical work; a
choreographic work or entertainment in dumb show; a
musical composition with or without words; a
cinematographic work to which are assimilated works
expressed by a process analogous to cinematography; a
work of drawing, painting, architecture, sculpture,
engraving or lithography; a photographic work to which
are assimilated works expressed by a process analogous to
photography; a work of applied art; an illustration, map,
plan, sketch or three-dimensional work relative to
geography, topography, architecture or science; a
performance; a broadcast; a phonogram; a compilation of
data to the extent it is protected as a copyrightable
work; or a work performed by a variety or circus
performer to the extent it is not otherwise considered a
literary or artistic work.</li>
<li><strong>""You""</strong> means an individual or entity
exercising rights under this License who has not
previously violated the terms of this License with
respect to the Work, or who has received express
permission from the Licensor to exercise rights under
this License despite a previous violation.</li>
<li><strong>""Publicly Perform""</strong> means to perform
public recitations of the Work and to communicate to the
public those public recitations, by any means or process,
including by wire or wireless means or public digital
performances; to make available to the public Works in
such a way that members of the public may access these
Works from a place and at a place individually chosen by
them; to perform the Work to the public by any means or
process and the communication to the public of the
performances of the Work, including by public digital
performance; to broadcast and rebroadcast the Work by any
means including signs, sounds or images.</li>
<li><strong>""Reproduce""</strong> means to make copies of
the Work by any means including without limitation by
sound or visual recordings and the right of fixation and
reproducing fixations of the Work, including storage of a
protected performance or phonogram in digital form or
other electronic medium.</li>
</ol>
<p><strong>2. Fair Dealing Rights.</strong> Nothing in this
License is intended to reduce, limit, or restrict any uses
free from copyright or rights arising from limitations or
exceptions that are provided for in connection with the
copyright protection under copyright law or other
applicable laws.</p>
<p><strong>3. License Grant.</strong> Subject to the terms
and conditions of this License, Licensor hereby grants You
a worldwide, royalty-free, non-exclusive, perpetual (for
the duration of the applicable copyright) license to
exercise the rights in the Work as stated below:</p>
<ol type=""a"">
<li>to Reproduce the Work, to incorporate the Work into
one or more Collections, and to Reproduce the Work as
incorporated in the Collections;</li>
<li>to create and Reproduce Adaptations provided that any
such Adaptation, including any translation in any medium,
takes reasonable steps to clearly label, demarcate or
otherwise identify that changes were made to the original
Work. For example, a translation could be marked ""The
original work was translated from English to Spanish,"" or
a modification could indicate ""The original work has been
modified."";</li>
<li>to Distribute and Publicly Perform the Work including
as incorporated in Collections; and,</li>
<li>to Distribute and Publicly Perform Adaptations.</li>
<li>
<p>For the avoidance of doubt:</p>
<ol type=""i"">
<li><strong>Non-waivable Compulsory License
Schemes</strong>. In those jurisdictions in which the
right to collect royalties through any statutory or
compulsory licensing scheme cannot be waived, the
Licensor reserves the exclusive right to collect such
royalties for any exercise by You of the rights
granted under this License;</li>
<li><strong>Waivable Compulsory License
Schemes</strong>. In those jurisdictions in which the
right to collect royalties through any statutory or
compulsory licensing scheme can be waived, the
Licensor waives the exclusive right to collect such
royalties for any exercise by You of the rights
granted under this License; and,</li>
<li><strong>Voluntary License Schemes</strong>. The
Licensor waives the right to collect royalties,
whether individually or, in the event that the
Licensor is a member of a collecting society that
administers voluntary licensing schemes, via that
society, from any exercise by You of the rights
granted under this License.</li>
</ol>
</li>
</ol>
<p>The above rights may be exercised in all media and
formats whether now known or hereafter devised. The above
rights include the right to make such modifications as are
technically necessary to exercise the rights in other media
and formats. Subject to Section 8(f), all rights not
expressly granted by Licensor are hereby reserved.</p>
<p><strong>4. Restrictions.</strong> The license granted in
Section 3 above is expressly made subject to and limited by
the following restrictions:</p>
<ol type=""a"">
<li>You may Distribute or Publicly Perform the Work only
under the terms of this License. You must include a copy
of, or the Uniform Resource Identifier (URI) for, this
License with every copy of the Work You Distribute or
Publicly Perform. You may not offer or impose any terms
on the Work that restrict the terms of this License or
the ability of the recipient of the Work to exercise the
rights granted to that recipient under the terms of the
License. You may not sublicense the Work. You must keep
intact all notices that refer to this License and to the
disclaimer of warranties with every copy of the Work You
Distribute or Publicly Perform. When You Distribute or
Publicly Perform the Work, You may not impose any
effective technological measures on the Work that
restrict the ability of a recipient of the Work from You
to exercise the rights granted to that recipient under
the terms of the License. This Section 4(a) applies to
the Work as incorporated in a Collection, but this does
not require the Collection apart from the Work itself to
be made subject to the terms of this License. If You
create a Collection, upon notice from any Licensor You
must, to the extent practicable, remove from the
Collection any credit as required by Section 4(b), as
requested. If You create an Adaptation, upon notice from
any Licensor You must, to the extent practicable, remove
from the Adaptation any credit as required by Section
4(b), as requested.</li>
<li>If You Distribute, or Publicly Perform the Work or
any Adaptations or Collections, You must, unless a
request has been made pursuant to Section 4(a), keep
intact all copyright notices for the Work and provide,
reasonable to the medium or means You are utilizing: (i)
the name of the Original Author (or pseudonym, if
applicable) if supplied, and/or if the Original Author
and/or Licensor designate another party or parties (e.g.,
a sponsor institute, publishing entity, journal) for
attribution (""Attribution Parties"") in Licensor's
copyright notice, terms of service or by other reasonable
means, the name of such party or parties; (ii) the title
of the Work if supplied; (iii) to the extent reasonably
practicable, the URI, if any, that Licensor specifies to
be associated with the Work, unless such URI does not
refer to the copyright notice or licensing information
for the Work; and (iv) , consistent with Section 3(b), in
the case of an Adaptation, a credit identifying the use
of the Work in the Adaptation (e.g., ""French translation
of the Work by Original Author,"" or ""Screenplay based on
original Work by Original Author""). The credit required
by this Section 4 (b) may be implemented in any
reasonable manner; provided, however, that in the case of
a Adaptation or Collection, at a minimum such credit will
appear, if a credit for all contributing authors of the
Adaptation or Collection appears, then as part of these
credits and in a manner at least as prominent as the
credits for the other contributing authors. For the
avoidance of doubt, You may only use the credit required
by this Section for the purpose of attribution in the
manner set out above and, by exercising Your rights under
this License, You may not implicitly or explicitly assert
or imply any connection with, sponsorship or endorsement
by the Original Author, Licensor and/or Attribution
Parties, as appropriate, of You or Your use of the Work,
without the separate, express prior written permission of
the Original Author, Licensor and/or Attribution
Parties.</li>
<li>Except as otherwise agreed in writing by the Licensor
or as may be otherwise permitted by applicable law, if
You Reproduce, Distribute or Publicly Perform the Work
either by itself or as part of any Adaptations or
Collections, You must not distort, mutilate, modify or
take other derogatory action in relation to the Work
which would be prejudicial to the Original Author's honor
or reputation. Licensor agrees that in those
jurisdictions (e.g. Japan), in which any exercise of the
right granted in Section 3(b) of this License (the right
to make Adaptations) would be deemed to be a distortion,
mutilation, modification or other derogatory action
prejudicial to the Original Author's honor and
reputation, the Licensor will waive or not assert, as
appropriate, this Section, to the fullest extent
permitted by the applicable national law, to enable You
to reasonably exercise Your right under Section 3(b) of
this License (right to make Adaptations) but not
otherwise.</li>
</ol>
<p><strong>5. Representations, Warranties and
Disclaimer</strong></p>
<p>UNLESS OTHERWISE MUTUALLY AGREED TO BY THE PARTIES IN
WRITING, LICENSOR OFFERS THE WORK AS-IS AND MAKES NO
REPRESENTATIONS OR WARRANTIES OF ANY KIND CONCERNING THE
WORK, EXPRESS, IMPLIED, STATUTORY OR OTHERWISE, INCLUDING,
WITHOUT LIMITATION, WARRANTIES OF TITLE, MERCHANTIBILITY,
FITNESS FOR A PARTICULAR PURPOSE, NONINFRINGEMENT, OR THE
ABSENCE OF LATENT OR OTHER DEFECTS, ACCURACY, OR THE
PRESENCE OF ABSENCE OF ERRORS, WHETHER OR NOT DISCOVERABLE.
SOME JURISDICTIONS DO NOT ALLOW THE EXCLUSION OF IMPLIED
WARRANTIES, SO SUCH EXCLUSION MAY NOT APPLY TO YOU.</p>
<p><strong>6. Limitation on Liability.</strong> EXCEPT TO
THE EXTENT REQUIRED BY APPLICABLE LAW, IN NO EVENT WILL
LICENSOR BE LIABLE TO YOU ON ANY LEGAL THEORY FOR ANY
SPECIAL, INCIDENTAL, CONSEQUENTIAL, PUNITIVE OR EXEMPLARY
DAMAGES ARISING OUT OF THIS LICENSE OR THE USE OF THE WORK,
EVEN IF LICENSOR HAS BEEN ADVISED OF THE POSSIBILITY OF
SUCH DAMAGES.</p>
<p><strong>7. Termination</strong></p>
<ol type=""a"">
<li>This License and the rights granted hereunder will
terminate automatically upon any breach by You of the
terms of this License. Individuals or entities who have
received Adaptations or Collections from You under this
License, however, will not have their licenses terminated
provided such individuals or entities remain in full
compliance with those licenses. Sections 1, 2, 5, 6, 7,
and 8 will survive any termination of this License.</li>
<li>Subject to the above terms and conditions, the
license granted here is perpetual (for the duration of
the applicable copyright in the Work). Notwithstanding
the above, Licensor reserves the right to release the
Work under different license terms or to stop
distributing the Work at any time; provided, however that
any such election will not serve to withdraw this License
(or any other license that has been, or is required to
be, granted under the terms of this License), and this
License will continue in full force and effect unless
terminated as stated above.</li>
</ol>
<p><strong>8. Miscellaneous</strong></p>
<ol type=""a"">
<li>Each time You Distribute or Publicly Perform the Work
or a Collection, the Licensor offers to the recipient a
license to the Work on the same terms and conditions as
the license granted to You under this License.</li>
<li>Each time You Distribute or Publicly Perform an
Adaptation, Licensor offers to the recipient a license to
the original Work on the same terms and conditions as the
license granted to You under this License.</li>
<li>If any provision of this License is invalid or
unenforceable under applicable law, it shall not affect
the validity or enforceability of the remainder of the
terms of this License, and without further action by the
parties to this agreement, such provision shall be
reformed to the minimum extent necessary to make such
provision valid and enforceable.</li>
<li>No term or provision of this License shall be deemed
waived and no breach consented to unless such waiver or
consent shall be in writing and signed by the party to be
charged with such waiver or consent.</li>
<li>This License constitutes the entire agreement between
the parties with respect to the Work licensed here. There
are no understandings, agreements or representations with
respect to the Work not specified here. Licensor shall
not be bound by any additional provisions that may appear
in any communication from You. This License may not be
modified without the mutual written agreement of the
Licensor and You.</li>
<li>The rights granted under, and the subject matter
referenced, in this License were drafted utilizing the
terminology of the Berne Convention for the Protection of
Literary and Artistic Works (as amended on September 28,
1979), the Rome Convention of 1961, the WIPO Copyright
Treaty of 1996, the WIPO Performances and Phonograms
Treaty of 1996 and the Universal Copyright Convention (as
revised on July 24, 1971). These rights and subject
matter take effect in the relevant jurisdiction in which
the License terms are sought to be enforced according to
the corresponding provisions of the implementation of
those treaty provisions in the applicable national law.
If the standard suite of rights granted under applicable
copyright law includes additional rights not granted
under this License, such additional rights are deemed to
be included in the License; this License is not intended
to restrict the license of any rights under applicable
law.</li>
</ol>
"
                };

                info.Add( binaryTheme );
            }

            {
                CreditsInfo fontAwesomeCss = new CreditsInfo()
                {
                    License = "MIT License",
                    LicenseUrl = "http://fontawesome.io/license/",
                    Purpose = "For site icons",
                    Title = "Font Awesome (CSS code)",
                    Url = "http://fontawesome.io/",
                    CodeUrl = "https://github.com/FortAwesome/Font-Awesome",
                    LicenseText =
@"
<p>Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the ""Software""), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:</p>

<p>The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.</p>

<p>THE SOFTWARE IS PROVIDED ""AS IS"", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.  IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE.</p>
"
                };

                CreditsInfo fontAwesomeFont = new CreditsInfo()
                {
                    License = "SIL OFL 1.1",
                    LicenseUrl = "http://fontawesome.io/license/",
                    Purpose = "For site icons",
                    Title = "Font Awesome (Webfont Files)",
                    Url = "http://fontawesome.io/",
                    CodeUrl = "https://github.com/FortAwesome/Font-Awesome",
                    LicenseText =
@"
<h2> SIL OPEN FONT LICENSE </h2>

<p>Version 1.1 - 26 February 2007</p>
<h3> PREAMBLE</h3>

<p>The goals of the Open Font License (OFL) are to stimulate worldwide<br />
development of collaborative font projects, to support the font creation<br />
efforts of academic and linguistic communities, and to provide a free and<br />
open framework in which fonts may be shared and improved in partnership<br />
with others.</p>
<p>The OFL allows the licensed fonts to be used, studied, modified and<br />
redistributed freely as long as they are not sold by themselves. The<br />
fonts, including any derivative works, can be bundled, embedded, <br />
redistributed and/or sold with any software provided that any reserved<br />
names are not used by derivative works. The fonts and derivatives,<br />
however, cannot be released under any other type of license. The<br />
requirement for fonts to remain under this license does not apply<br />
to any document created using the fonts or their derivatives.</p>
<h3> DEFINITIONS</h3>

<p>""Font Software"" refers to the set of files released by the Copyright<br />
Holder(s) under this license and clearly marked as such. This may<br />
include source files, build scripts and documentation.</p>
<p>""Reserved Font Name"" refers to any names specified as such after the<br />
copyright statement(s).</p>
<p>""Original Version"" refers to the collection of Font Software components as<br />
distributed by the Copyright Holder(s).</p>
<p>""Modified Version"" refers to any derivative made by adding to, deleting,<br />
or substituting &mdash; in part or in whole &mdash; any of the components of the<br />
Original Version, by changing formats or by porting the Font Software to a<br />
new environment.</p>
<p>""Author"" refers to any designer, engineer, programmer, technical<br />
writer or other person who contributed to the Font Software.</p>
<h3>PERMISSION &amp; CONDITIONS</h3>

<p>Permission is hereby granted, free of charge, to any person obtaining<br />
a copy of the Font Software, to use, study, copy, merge, embed, modify,<br />
redistribute, and sell modified and unmodified copies of the Font<br />
Software, subject to the following conditions:</p>
<p>1) Neither the Font Software nor any of its individual components,<br />
in Original or Modified Versions, may be sold by itself.</p>
<p>2) Original or Modified Versions of the Font Software may be bundled,<br />
redistributed and/or sold with any software, provided that each copy<br />
contains the above copyright notice and this license. These can be<br />
included either as stand-alone text files, human-readable headers or<br />
in the appropriate machine-readable metadata fields within text or<br />
binary files as long as those fields can be easily viewed by the user.</p>
<p>3) No Modified Version of the Font Software may use the Reserved Font<br />
Name(s) unless explicit written permission is granted by the corresponding<br />
Copyright Holder. This restriction only applies to the primary font name as<br />
presented to the users.</p>
<p>4) The name(s) of the Copyright Holder(s) or the Author(s) of the Font<br />
Software shall not be used to promote, endorse or advertise any<br />
Modified Version, except to acknowledge the contribution(s) of the<br />
Copyright Holder(s) and the Author(s) or with their explicit written<br />
permission.</p>
<p>5) The Font Software, modified or unmodified, in part or in whole,<br />
must be distributed entirely under this license, and must not be<br />
distributed under any other license. The requirement for fonts to<br />
remain under this license does not apply to any document created<br />
using the Font Software.</p>
<h3>TERMINATION</h3>

<p>This license becomes null and void if any of the above conditions are<br />
not met.</p>
<h3>DISCLAIMER</h3>

<p>THE FONT SOFTWARE IS PROVIDED ""AS IS"", WITHOUT WARRANTY OF ANY KIND,<br />
EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO ANY WARRANTIES OF<br />
MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT<br />
OF COPYRIGHT, PATENT, TRADEMARK, OR OTHER RIGHT. IN NO EVENT SHALL THE<br />
COPYRIGHT HOLDER BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY,<br />
INCLUDING ANY GENERAL, SPECIAL, INDIRECT, INCIDENTAL, OR CONSEQUENTIAL<br />
DAMAGES, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING<br />
FROM, OUT OF THE USE OR INABILITY TO USE THE FONT SOFTWARE OR FROM<br />
OTHER DEALINGS IN THE FONT SOFTWARE.</p>
"
                };

                info.Add( fontAwesomeCss );
                info.Add( fontAwesomeFont );
            }

            {
                CreditsInfo jquery = new CreditsInfo()
                {
                    CodeUrl = "https://github.com/jquery/jquery",
                    License = "MIT License",
                    LicenseUrl = "https://github.com/jquery/jquery/blob/master/LICENSE.txt",
                    Purpose = "UI effects",
                    Title = "JQuery",
                    Url = "http://jquery.com/",
                    LicenseText =
@"
<p>Copyright JS Foundation and other contributors, https://js.foundation/</p>

<p>This software consists of voluntary contributions made by many
individuals. For exact contribution history, see the revision history
available at https://github.com/jquery/jquery</p>

<p>The following license applies to all parts of this software except as
documented below:</p>

<p>====</p>

<p>Permission is hereby granted, free of charge, to any person obtaining
a copy of this software and associated documentation files (the
""Software""), to deal in the Software without restriction, including
without limitation the rights to use, copy, modify, merge, publish,
distribute, sublicense, and/or sell copies of the Software, and to
permit persons to whom the Software is furnished to do so, subject to
the following conditions:</p>

<p>The above copyright notice and this permission notice shall be
included in all copies or substantial portions of the Software.</p>

<p>THE SOFTWARE IS PROVIDED ""AS IS"", WITHOUT WARRANTY OF ANY KIND,
EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.</p>

<p>====</p>

<p>All files located in the node_modules and external directories are
externally maintained libraries used by this software which have their
own licenses; we recommend you read them, as their terms may differ from
the terms above.</p>
"
                };
                info.Add( jquery );
            }

            {
                CreditsInfo bootstrap = new CreditsInfo()
                {
                    CodeUrl = "https://github.com/twbs/bootstrap",
                    License = "MIT License",
                    LicenseUrl = "https://github.com/twbs/bootstrap/blob/v4-dev/LICENSE",
                    Purpose = "CSS Themeing",
                    Title = "Bootstrap",
                    Url = "http://getbootstrap.com/",
                    LicenseText =
@"
<p>Copyright (c) 2011-2017 Twitter, Inc.</p>
<p>Copyright (c) 2011-2017 The Bootstrap Authors</p>

<p>Permission is hereby granted, free of charge, to any person obtaining
a copy of this software and associated documentation files (the
""Software""), to deal in the Software without restriction, including
without limitation the rights to use, copy, modify, merge, publish,
distribute, sublicense, and/or sell copies of the Software, and to
permit persons to whom the Software is furnished to do so, subject to
the following conditions:</p>

<p>The above copyright notice and this permission notice shall be
included in all copies or substantial portions of the Software.</p>

<p>THE SOFTWARE IS PROVIDED ""AS IS"", WITHOUT WARRANTY OF ANY KIND,
EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.</p>
"
                };
                info.Add( bootstrap );
            }

            {
                CreditsInfo litedb = new CreditsInfo()
                {
                    CodeUrl = "https://github.com/mbdavid/LiteDB",
                    License = "MIT License",
                    LicenseUrl = "https://github.com/mbdavid/LiteDB/blob/master/LICENSE",
                    Purpose = "Database to save sessions.",
                    Title = "LiteDB",
                    Url = "https://github.com/mbdavid/LiteDB",
                    LicenseText =
@"
<p>Copyright (c) 2014-2015 Mauricio David</p>

<p>Permission is hereby granted, free of charge, to any person obtaining
a copy of this software and associated documentation files (the
""Software""), to deal in the Software without restriction, including
without limitation the rights to use, copy, modify, merge, publish,
distribute, sublicense, and/or sell copies of the Software, and to
permit persons to whom the Software is furnished to do so, subject to
the following conditions:</p>

<p>The above copyright notice and this permission notice shall be
included in all copies or substantial portions of the Software.</p>

<p>THE SOFTWARE IS PROVIDED ""AS IS"", WITHOUT WARRANTY OF ANY KIND,
EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.</p>
"
                };
                info.Add( litedb );
            }

            {
                CreditsInfo electronNet = new CreditsInfo()
                {
                    CodeUrl = "https://github.com/ElectronNET/Electron.NET",
                    License = "MIT License",
                    LicenseUrl = "https://github.com/ElectronNET/Electron.NET/blob/master/LICENSE",
                    Purpose = "Powers Desktop Application",
                    Title = "Electron.NET",
                    Url = "https://github.com/ElectronNET/Electron.NET",
                    LicenseText =
@"
<p>Copyright (c) 2017 Gregor Biswanger, Robert Mühsig</p>

<p>Permission is hereby granted, free of charge, to any person obtaining
a copy of this software and associated documentation files (the
""Software""), to deal in the Software without restriction, including
without limitation the rights to use, copy, modify, merge, publish,
distribute, sublicense, and/or sell copies of the Software, and to
permit persons to whom the Software is furnished to do so, subject to
the following conditions:</p>

<p>The above copyright notice and this permission notice shall be
included in all copies or substantial portions of the Software.</p>

<p>THE SOFTWARE IS PROVIDED ""AS IS"", WITHOUT WARRANTY OF ANY KIND,
EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.</p>
"
                };
                info.Add( electronNet );
            }

            {
                CreditsInfo sethCs = new CreditsInfo()
                {
                    CodeUrl = "https://github.com/xforever1313/sethcs",
                    License = "Boost Software License - Version 1.0",
                    LicenseUrl = "https://github.com/xforever1313/sethcs/blob/master/LICENSE_1_0.txt",
                    Purpose = "C# Library for common operations",
                    Title = "SethCS",
                    Url = "https://github.com/xforever1313/sethcs",
                    LicenseText =
@"
<p>Boost Software License - Version 1.0 - August 17th, 2003</p>

<p>Permission is hereby granted, free of charge, to any person or organization
obtaining a copy of the software and accompanying documentation covered by
this license (the ""Software"") to use, reproduce, display, distribute,
execute, and transmit the Software, and to prepare derivative works of the
Software, and to permit third-parties to whom the Software is furnished to
do so, all subject to the following:</p>

<p>The copyright notices in the Software and this entire statement, including
the above license grant, this restriction and the following disclaimer,
must be included in all copies of the Software, in whole or in part, and
all derivative works of the Software, unless such copies or derivative
works are solely in the form of machine-executable object code generated by
a source language processor.</p>

<p>THE SOFTWARE IS PROVIDED ""AS IS"", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE, TITLE AND NON-INFRINGEMENT. IN NO EVENT
SHALL THE COPYRIGHT HOLDERS OR ANYONE DISTRIBUTING THE SOFTWARE BE LIABLE
FOR ANY DAMAGES OR OTHER LIABILITY, WHETHER IN CONTRACT, TORT OR OTHERWISE,
ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER
DEALINGS IN THE SOFTWARE.</p>
"
                };
                info.Add( sethCs );
            }

            {
                CreditsInfo leaflet = new CreditsInfo()
                {
                    CodeUrl = "https://github.com/Leaflet/Leaflet",
                    License = "BSD 2-clause \"Simplified\" License",
                    LicenseUrl = "https://github.com/Leaflet/Leaflet/blob/master/LICENSE",
                    Purpose = "Map Views",
                    Title = "Leaflet.js",
                    Url = "http://leafletjs.com/",
                    LicenseText =
@"
<p>Copyright (c) 2010-2017, Vladimir Agafonkin</p>
<p>Copyright (c) 2010-2011, CloudMade</p>
<p>All rights reserved.</p>

<p>Redistribution and use in source and binary forms, with or without modification, are
permitted provided that the following conditions are met:</p>

   <p>1. Redistributions of source code must retain the above copyright notice, this list of
      conditions and the following disclaimer.</p>

   <p>2. Redistributions in binary form must reproduce the above copyright notice, this list
      of conditions and the following disclaimer in the documentation and/or other materials
      provided with the distribution.</p>

<p>THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS ""AS IS"" AND ANY
EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF
MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE
COPYRIGHT HOLDER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL,
EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF
SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION)
HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR
TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS
SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.</p>
"
                };
                info.Add( leaflet );
            }
            {
                CreditsInfo chartjs = new CreditsInfo()
                {
                    CodeUrl = "https://github.com/chartjs/Chart.js",
                    License = "MIT License",
                    LicenseUrl = "https://github.com/chartjs/Chart.js/blob/master/LICENSE.md",
                    Purpose = "Graph Views",
                    Title = "Graph.js",
                    Url = "http://www.chartjs.org/",
                    LicenseText =
@"
<p>The MIT License (MIT)</p>

<p>Copyright (c) 2013-2017 Nick Downie</p>

<p>Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the ""Software""), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:</p>

<p>The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.</p>

<p>THE SOFTWARE IS PROVIDED ""AS IS"", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.</p>
"
                };
                info.Add( chartjs );
            }

            {
                CreditsInfo momentJs = new CreditsInfo()
                {
                    CodeUrl = "https://github.com/moment/moment/",
                    License = "MIT License",
                    LicenseUrl = "https://github.com/moment/moment/blob/master/LICENSE",
                    Purpose = "Graph and Calendar views",
                    Title = "Moment.js",
                    Url = "http://momentjs.com/",
                    LicenseText =
@"
<p>Copyright (c) JS Foundation and other contributors</p>

<p>Permission is hereby granted, free of charge, to any person
obtaining a copy of this software and associated documentation
files (the ""Software""), to deal in the Software without
restriction, including without limitation the rights to use,
copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the
Software is furnished to do so, subject to the following
conditions:</p>

<p>The above copyright notice and this permission notice shall be
included in all copies or substantial portions of the Software.</p>

<p>THE SOFTWARE IS PROVIDED ""AS IS"", WITHOUT WARRANTY OF ANY KIND,
EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES
OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT
HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY,
WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR
OTHER DEALINGS IN THE SOFTWARE.</p>
"
                };
                info.Add( momentJs );
            }
        }

        // ---------------- Properties ----------------

        /// <summary>
        /// Title of the thing being credited.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Why are we using this project?
        /// </summary>
        public string Purpose { get; set; }

        /// <summary>
        /// URL to more information about the project.
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// The License Type
        /// </summary>
        public string License { get; set; }

        /// <summary>
        /// Text of the license.
        /// </summary>
        public string LicenseText { get; set; }

        /// <summary>
        /// URL to the license.
        /// </summary>
        public string LicenseUrl { get; set; }

        /// <summary>
        /// URL to the code or source.
        /// </summary>
        public string CodeUrl { get; set; }

        /// <summary>
        /// Read-only list of all credits.
        /// </summary>
        public static IReadOnlyList<CreditsInfo> AllCredits { get; private set; }
    }
}
