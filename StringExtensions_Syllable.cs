// *****************************************************
//                      EXTENSIONS     
//                  by  Shane Whitehead
//                  bwakabats@gmail.com
// *****************************************************
//      The software is released under the GNU GPL:
//          http://www.gnu.org/licenses/gpl.txt
//
// Feel free to use, modify and distribute this software
// I only ask you to keep this comment intact.
// Please contact me with bugs, ideas, modification etc.
// *****************************************************
using System;
using System.Collections.Generic;

namespace BWakaBats.Extensions
{
    public static partial class StringExtensions
    {
        #region ToSyllables Constants

        private static readonly string[] _phonemes = new string[]
        {
            "str", "tch", "thr",
            "ck", "dg", "lk", "th",
            "ch", "gh", "kn", "ph", "sh", "wh", "wr"
        };

        private static readonly string[] _prefixes = new string[]
        {
        };

        private static readonly string[] _suffixes = new string[]
        {
            "hood", "tion", "tive",
            "ing", "cal", "est", "tle", "ish", "age", "ble", "ous", "tic", "bly",
            "er", "ic"
        };

        private const string _shortVowels = ";atcha;atche;atchi;atcho;atchu;atchy;etcha;etche;etchi;etcho;etchu;etchy;itcha;itche;itchi;itchu;itchy;otcha;otche;otchi;otcho;otchu;otchy;utcha;utche;utchi;utchu;utchy;ytcha;ytche;ytchi;ytchu;ytchy"
                                          + ";acka;acke;acki;acko;acku;acky;ecka;ecke;ecki;ecko;ecku;ecky;icka;icke;icki;icko;icku;icky;ocka;ocke;ocki;ocko;ocku;ocky;ucka;ucke;ucki;ucko;ucku;ucky;ycka;ycke;ycki;ycko;ycku;ycky"
                                          + ";adga;adge;adgi;adgo;adgu;adgy;edga;edge;edgi;edgo;edgu;edgy;idga;idge;idgi;idgo;idgu;idgy;odga;odge;odgi;odgo;odgu;odgy;udga;udge;udgi;udgo;udgu;udgy;ydga;ydge;ydgi;ydgo;ydgu;ydgy"
                                          + ";alka;alke;alki;alko;alku;alky;elka;elke;elki;elko;elku;elky;ilka;ilke;ilki;ilko;ilku;ilky;olka;olke;olki;olko;olku;olky;ulka;ulke;ulki;ulko;ulku;ulky;ylka;ylke;ylki;ylko;ylku;ylky"
                                          + ";athe;athi;atho;ethe;ethi;ethy;itha;ithe;ithi;itho;ithy;otha;othe;othi;uthe;uthi;uthy"
                                          + ";acha;ache;achy;eche;echo;echy;icha;ichi;ichu;ichy;ochi;ochy;ucha;uchi;uchy"
                                          + ";aghi;agho;aghy;egha;eghe;eghi;egho;eghu;eghy;igha;ighe;ighi;igho;ighu;ighy;ogha;oghe;oghi;ogho;oghu;oghy;ugha;ughe;ughi;ugho;ughu;ughy;ygha;yghe;yghi;ygho;yghu;yghy"
                                          + ";aphi;ephy;ipha;ophe;ophi;yphi"
                                          + ";ashe;ashi;ashu;ashy;eshe;eshy;isha;ishe;ishi;isho;ishu;ishy;osha;oshe;oshi;osho;oshu;oshy;usha;ushe;ushi;usho;ushu;ushy;yshy"
                                          + ";iba;ibi;ibu;iby;obu;ubo;aci;acu;icu;yce;ade;adi;ede;edi;idi;idu;ode;odi;ody;ude;udu;udy;afe;afi;afy;ife;ofi;ofy"
                                          + ";agi;egu;igi;igo;igu;yga;aka;ake;aki;ako;aky;eky;ike;iki;iko;iky;oke;oki;oko;oky;uki;uky;yke;yki;yko;yky;ali;alu;ili;olo;oly;yli"
                                          + ";ami;amy;emi;imi;imu;imy;ome;omi;umy;ymi;api;apu;epi;epy;ipi;ipy;ope;opi;opu;opy;ypi;ara;are;ari;aru;ary;era;ere;eri;ero;ery;iri;iry;oro;yru"
                                          + ";asa;ase;asu;isa;ise;isi;isu;osa;ose;usa;use;usy;ate;eti;ite;ote;ute;yti;ave;avi;avy;eve;evy;ive;ivi"
                                          + ";awe;awi;awu;awy;ewe;ewu;ewy;iwu;iwy;owa;owe;owi;owo;owu;owy;uwu;uwy;ywu;ywy"
                                          + ";axa;axe;axi;axo;axu;axy;exa;exe;exi;exo;exu;exy;ixa;ixe;ixi;ixo;ixu;ixy;oxa;oxe;oxi;oxo;oxu;oxy;uxa;uxe;uxi;uxo;uxu;uxy;yxa;yxe;yxi;yxo;yxu;yxy"
                                          + ";aza;eza;eze;ezi;ezu;ezy;iza;ize;izi;izy;oze;ozi;ozu;ozy;uza;uzi;uzy;yza;yze;yzi;yzu;yzy;";

        private const string _splitBefore = ";bstrr;cstrr;dstrr;fstrr;hstrr;jstrr;kstrj;kstrl;kstrr;kstrv;kstrx;kstrz;lstrj;lstrr;lstrv;lstrx;lstrz;mstrj;mstrl;mstrr;mstrv;mstrx;mstrz;nstrj;nstrr;nstrv;nstrx;nstrz;pstrr;qstrr;rstrr;tstrr;vstrr;wstrr;xstrr;zstrr"
                                          + ";btchr;ctchr;dtchr;ftchr;htchr;jtchr;ktchj;ktchl;ktchr;ktchv;ktchx;ktchz;ltchj;ltchr;ltchv;ltchx;ltchz;mtchj;mtchl;mtchr;mtchv;mtchx;mtchz;ntchj;ntchr;ntchv;ntchx;ntchz;ptchr;qtchr;rtchr;ttchr;vtchr;wtchr;xtchr;ztchr"
                                          + ";bthrr;cthrr;dthrr;fthrr;hthrr;jthrr;kthrj;kthrl;kthrr;kthrv;kthrx;kthrz;lthrj;lthrr;lthrv;lthrx;lthrz;mthrj;mthrl;mthrr;mthrv;mthrx;mthrz;nthrj;nthrr;nthrv;nthrx;nthrz;pthrr;qthrr;rthrr;tthrr;vthrr;wthrr;xthrr;zthrr"
                                          + ";bckr;cckr;dckr;fckr;hckr;jckr;kckj;kckl;kckr;kckv;kckx;kckz;lckj;lckr;lckv;lckx;lckz;mckj;mckl;mckr;mckv;mckx;mckz;nckj;nckr;nckv;nckx;nckz;pckr;qckr;rckr;tckr;vckr;wckr;xckr;zckr"
                                          + ";bdgr;cdgr;ddgr;fdgr;hdgr;jdgr;kdgj;kdgl;kdgr;kdgv;kdgx;kdgz;ldgj;ldgr;ldgv;ldgx;ldgz;mdgj;mdgl;mdgr;mdgv;mdgx;mdgz;ndgj;ndgr;ndgv;ndgx;ndgz;pdgr;qdgr;rdgr;tdgr;vdgr;wdgr;xdgr;zdgr"
                                          + ";blkr;clkr;dlkr;flkr;hlkr;jlkr;klkj;klkl;klkr;klkv;klkx;klkz;llkj;llkr;llkv;llkx;llkz;mlkj;mlkl;mlkr;mlkv;mlkx;mlkz;nlkj;nlkr;nlkv;nlkx;nlkz;plkr;qlkr;rlkr;tlkr;vlkr;wlkr;xlkr;zlkr"
                                          + ";bthr;cthr;dthr;fthr;hthr;jthr;kthc;kthg;kthh;kthj;kthk;kthn;kthr;ktht;kthv;kthx;kthz;mthr;nthc;nthg;nthh;nthj;nthk;nthn;nthr;ntht;nthv;nthx;nthz;pthr;qthr;rthr;tthc;tthg;tthh;tthj;tthk;tthn;tthr;ttht;tthv;tthx;tthz;vthr;wthr;xthr;zthr"
                                          + ";bchr;cchr;dchr;fchr;gchr;hchr;jchr;kchr;lchj;lchq;lchr;lchs;lchv;lchx;lchz;mchr;nchr;pchr;qchr;schr;vchr;wchr;xchr;zchr"
                                          + ";bghr;cghr;dghr;fghr;hghr;jghr;kghj;kghl;kghr;kghv;kghx;kghz;lghj;lghr;lghv;lghx;lghz;mghj;mghl;mghr;mghv;mghx;mghz;nghj;nghr;nghv;nghx;nghz;pghr;qghr;rghr;tghr;vghr;wghr;xghr;zghr"
                                          + ";bknr;cknr;dknr;fknr;hknr;jknr;kknj;kknl;kknr;kknv;kknx;kknz;lknj;lknr;lknv;lknx;lknz;mknj;mknl;mknr;mknv;mknx;mknz;nknj;nknr;nknv;nknx;nknz;pknr;qknr;rknr;tknr;vknr;wknr;xknr;zknr"
                                          + ";bphb;bphc;bphd;bphf;bphg;bphh;bphj;bphk;bphl;bphm;bphn;bphp;bphq;bphr;bphs;bpht;bphv;bphw;bphx;bphz;cphb;cphc;cphd;cphf;cphg;cphh;cphj;cphk;cphl;cphm;cphn;cphp;cphq;cphr;cphs;cpht;cphv;cphw;cphx;cphz;dphb;dphc;dphd;dphf;dphg;dphh;dphj;dphk;dphl;dphm;dphn;dphp;dphq;dphr;dphs;dpht;dphv;dphw;dphx;dphz"
                                          + ";fphb;fphc;fphd;fphf;fphg;fphh;fphj;fphk;fphl;fphm;fphn;fphp;fphq;fphr;fphs;fpht;fphv;fphw;fphx;fphz;gphb;gphc;gphd;gphf;gphg;gphh;gphj;gphk;gphl;gphm;gphn;gphp;gphq;gphr;gphs;gpht;gphv;gphw;gphx;gphz;hphb;hphc;hphd;hphf;hphg;hphh;hphj;hphk;hphl;hphm;hphn;hphp;hphq;hphr;hphs;hpht;hphv;hphw;hphx;hphz"
                                          + ";jphb;jphc;jphd;jphf;jphg;jphh;jphj;jphk;jphl;jphm;jphn;jphp;jphq;jphr;jphs;jpht;jphv;jphw;jphx;jphz;kphb;kphc;kphd;kphf;kphg;kphh;kphj;kphk;kphl;kphm;kphn;kphp;kphq;kphr;kphs;kpht;kphv;kphw;kphx;kphz;lphb;lphc;lphd;lphf;lphg;lphh;lphj;lphk;lphl;lphm;lphn;lphp;lphq;lphr;lphs;lpht;lphv;lphw;lphx;lphz"
                                          + ";mphb;mphc;mphd;mphf;mphg;mphh;mphj;mphk;mphl;mphm;mphn;mphp;mphq;mphr;mphs;mpht;mphv;mphw;mphx;mphz;nphb;nphc;nphd;nphf;nphg;nphh;nphj;nphk;nphl;nphm;nphn;nphp;nphq;nphr;nphs;npht;nphv;nphw;nphx;nphz;pphb;pphc;pphd;pphf;pphg;pphh;pphj;pphk;pphl;pphm;pphn;pphp;pphq;pphr;pphs;ppht;pphv;pphw;pphx;pphz"
                                          + ";qphb;qphc;qphd;qphf;qphg;qphh;qphj;qphk;qphl;qphm;qphn;qphp;qphq;qphr;qphs;qpht;qphv;qphw;qphx;qphz;rphb;rphc;rphd;rphf;rphg;rphh;rphj;rphk;rphl;rphm;rphn;rphp;rphq;rphr;rphs;rpht;rphv;rphw;rphx;rphz;sphb;sphc;sphd;sphf;sphg;sphh;sphj;sphk;sphl;sphm;sphn;sphp;sphq;sphr;sphs;spht;sphv;sphw;sphx;sphz"
                                          + ";tphb;tphc;tphd;tphf;tphg;tphh;tphj;tphk;tphl;tphm;tphn;tphp;tphq;tphr;tphs;tpht;tphv;tphw;tphx;tphz;vphb;vphc;vphd;vphf;vphg;vphh;vphj;vphk;vphl;vphm;vphn;vphp;vphq;vphr;vphs;vpht;vphv;vphw;vphx;vphz;wphb;wphc;wphd;wphf;wphg;wphh;wphj;wphk;wphl;wphm;wphn;wphp;wphq;wphr;wphs;wpht;wphv;wphw;wphx;wphz"
                                          + ";xphb;xphc;xphd;xphf;xphg;xphh;xphj;xphk;xphl;xphm;xphn;xphp;xphq;xphr;xphs;xpht;xphv;xphw;xphx;xphz;zphb;zphc;zphd;zphf;zphg;zphh;zphj;zphk;zphl;zphm;zphn;zphp;zphq;zphr;zphs;zpht;zphv;zphw;zphx;zphz"
                                          + ";bshr;cshr;dshr;fshr;gshr;hshr;jshr;kshj;kshr;kshv;kshx;kshz;lshj;lshr;lshv;lshx;lshz;mshj;mshr;mshv;mshx;mshz;nshb;nshc;nshd;nshf;nshg;nshh;nshj;nshk;nshp;nshq;nshr;nshs;nsht;nshv;nshw;nshx;nshz;pshr;qshr;rshr;sshr;tshr;vshr;wshr;xshr;zshr"
                                          + ";bwhr;cwhr;dwhr;fwhr;hwhr;jwhr;kwhj;kwhl;kwhr;kwhv;kwhx;kwhz;lwhj;lwhr;lwhv;lwhx;lwhz;mwhj;mwhl;mwhr;mwhv;mwhx;mwhz;nwhj;nwhr;nwhv;nwhx;nwhz;pwhr;qwhr;rwhr;twhr;vwhr;wwhr;xwhr;zwhr"
                                          + ";bwrr;cwrr;dwrr;fwrr;hwrr;jwrr;kwrj;kwrl;kwrr;kwrv;kwrx;kwrz;lwrj;lwrr;lwrv;lwrx;lwrz;mwrj;mwrl;mwrr;mwrv;mwrx;mwrz;nwrj;nwrr;nwrv;nwrx;nwrz;pwrr;qwrr;rwrr;twrr;vwrr;wwrr;xwrr;zwrr;";

        #endregion

        #region LookupDictionary

        class LookupDictionary : Dictionary<string, string>
        {
            public LookupDictionary() : base(StringComparer.OrdinalIgnoreCase) { }

            public void Add(string syllables)
            {
                base.Add(syllables.Replace(" ", ""), syllables);
            }
        }

        #endregion

        /// <summary>
        /// Returns a new string in which all the syllables have been prefixed with a seperator.
        /// NOTE: This is only an educated guess
        /// </summary>
        /// <param name="source">The source string</param>
        /// <param name="separator">The seperator to use</param>
        /// <returns>The syllables</returns>
        public static string ToSyllables(this string source, string separator = " ")
        {
            List<string> syllables = new List<string>(); ;
            string[] words = source.ToWords().Split(' ');
            foreach (var word in words)
            {
                var wordSyllables = ToSyllablesInternal(word, separator);
                syllables.AddRange(wordSyllables);
            }
            return string.Join(separator, syllables);
        }

        #region ToSyllables Helpers

        private static IEnumerable<string> ToSyllablesInternal(string word, string seperator)
        {
            List<string> syllables = new List<string>(); ;
            List<int> counts = new List<int>();

            int first;
            if (word.StartsWith("Mc", StringComparison.OrdinalIgnoreCase)) // McHack
            {
                syllables.Add(word.Substring(0, 2));
                word = word.Substring(2);
                first = 1;
            }
            else
            {
                first = 0;
            }

            string prefix;
            string suffix;
            word = FindPrefixAndSuffix(word, out prefix, out suffix);
            if (!string.IsNullOrEmpty(word))
            {
                FindSyllables(word, syllables, counts);
                if (counts.Count > 2)
                {
                    JoinSyllables(syllables, counts);
                }
                JoinE(syllables);
            }
            if (prefix != null)
            {
                if (seperator != " ")
                {
                    prefix = prefix.Replace(" ", seperator);
                }
                syllables.Insert(first, prefix);
            }
            if (suffix != null)
            {
                if (seperator != " ")
                {
                    suffix = suffix.Replace(" ", seperator);
                }
                syllables.Add(suffix);
            }
            return syllables;
        }

        private static string FindPrefixAndSuffix(string word, out string prefix, out string suffix)
        {
            string syllables;
            if (_syllableLookup.TryGetValue(word, out syllables))
            {
                prefix = FixCase(word, syllables);
                suffix = null;
                return null;
            }

            int length = word.Length;
            string bestPrefixWord = null;
            string bestPrefixSyllables = null;
            string bestSuffixWord = null;
            string bestSuffixSyllables = null;
            for (int index = 2; index < length - 2; index++)
            {
                string start = word.Substring(0, index);
                string startSyllables;
                bool hasStart = _syllableLookup.TryGetValue(start, out startSyllables);

                string end = word.Substring(index);
                string endSyllables;
                bool hasEnd = _syllableLookup.TryGetValue(end, out endSyllables);

                if (hasStart)
                {
                    if (hasEnd)
                    {
                        prefix = FixCase(start, startSyllables);
                        suffix = FixCase(end, endSyllables);
                        return null;
                    }
                    if (bestSuffixWord == null || index + bestSuffixWord.Length < length)
                    {
                        bestPrefixWord = start;
                        bestPrefixSyllables = FixCase(start, startSyllables);
                    }
                    else if (index + bestSuffixWord.Length < length)
                    {
                        Console.WriteLine("stop");
                    }
                }
                else if (hasEnd && bestSuffixWord == null)
                {
                    bestSuffixWord = end;
                    bestSuffixSyllables = endSyllables;
                }
            }

            prefix = null;
            suffix = null;
            if (bestSuffixWord != null)
            {
                suffix = FixCase(bestSuffixWord, bestSuffixSyllables);
                word = word.Substring(0, length - bestSuffixWord.Length);
            }
            if (bestPrefixWord != null)
            {
                prefix = FixCase(bestPrefixWord, bestPrefixSyllables);
                word = word.Substring(bestPrefixWord.Length);
                return word;
            }
            if (bestSuffixWord != null)
                return word;

            foreach (var value in _suffixes)
            {
                if (word.EndsWith(value, StringComparison.OrdinalIgnoreCase))
                {
                    suffix = FixCase(word.Substring(length - value.Length), value);
                    word = word.Substring(0, length - value.Length);
                    break;
                }
            }

            foreach (var value in _prefixes)
            {
                if (word.StartsWith(value, StringComparison.OrdinalIgnoreCase))
                {
                    prefix = FixCase(word, value);
                    word = word.Substring(value.Length);
                    break;
                }
            }

            return word;
        }

        private static string FixCase(string original, string syllables)
        {
            int length = original.Length;
            int indexSyllables = 0;
            string output = "";
            for (int indexOriginal = 0; indexOriginal < length; indexOriginal++)
            {
                char chr = original[indexOriginal];
                while (syllables[indexSyllables] == ' ')
                {
                    output += " ";
                    indexSyllables++;
                }
                if (char.IsUpper(chr))
                {
                    output += Char.ToUpperInvariant(chr);
                }
                else
                {
                    output += chr;
                }
                indexSyllables++;
            }
            return output;
        }

        private static void FindSyllables(string source, List<string> syllables, List<int> counts)
        {
            const char VOWEL = 'V';
            const char CONSONANT = 'C';

            int length = source.Length;
            string changed = source.ToLowerInvariant();

            foreach (var phoneme in _phonemes)
            {
                // x is a consonant and does not end any phonemes above...
                if (phoneme.Length == 3)
                {
                    changed = changed.Replace(phoneme, "x  ");
                }
                else
                {
                    changed = changed.Replace(phoneme, "x ");
                }
            }

            var chrs = changed.ToCharArray();
            var previousOne = CONSONANT;
            var previousGroup = CONSONANT;
            string syllable = "";
            int count = 0;

            for (int index = 0; index < length; index++)
            {
                char original = chrs[index];
                char type;
                if (original == ' ')
                {
                    type = ' ';
                }
                else if (index == length - 1 && original == 'e')
                {
                    type = ' ';
                }
                else
                {
                    //      abcdefghijklmnopqrstuvwxyz
                    type = "VCCCVCCCVCCCCCVCCCCCVCCCVC"[original - 'a'];
                    if (previousOne == VOWEL && type == VOWEL)
                    {
                        type = ' ';
                    }
                }
                if (type != ' ')
                {
                    if (!string.IsNullOrEmpty(syllable))
                    {
                        syllables.Add(syllable);
                    }
                    syllable = "";
                    if (previousGroup != type)
                    {
                        if (count == 0)
                        {
                            syllables.Add("");
                            count = 1;
                        }
                        counts.Add(count);
                        count = 0;
                        previousGroup = type;
                    }
                    count++;
                    previousOne = type;
                }
                syllable += source[index];
            }
            syllables.Add(syllable);
            if (previousGroup == VOWEL)
            {
                syllables.Add("");
                counts.Add(1);
                count = 1;
            }
            else if (count > 1)
            {
                syllable = JoinSyllables(syllables, count, syllables.Count - count);
                syllables.Add(syllable);
                count = 1;
            }
            counts.Add(count);
        }

        private static void JoinSyllables(List<string> syllables, List<int> counts)
        {
            int countIndex = 0;
            int syllableIndex = 0;
            while (countIndex < counts.Count - 1)
            {
                int count = counts[countIndex];
                string syllable = JoinSyllables(syllables, count, syllableIndex);
                counts.RemoveAt(countIndex);
                count = counts[countIndex];
                syllable += JoinSyllables(syllables, count, syllableIndex);
                counts.RemoveAt(countIndex);
                count = counts[countIndex];
                int remainder;
                if (count > 1)
                {
                    if (count % 2 == 1)
                    {
                        int midIndex = syllableIndex + (count - 1) / 2;

                        string previousSyllable = syllables[midIndex - 1];
                        char previous = previousSyllable[previousSyllable.Length - 1];
                        char next = syllables[midIndex + 1][0];
                        string lookup = ";" + previous + syllables[midIndex] + next + ";";
                        if (_splitBefore.IndexOf(lookup, StringComparison.OrdinalIgnoreCase) > -1)
                        {
                            count--;
                        }
                        else
                        {
                            count++;
                        }
                    }
                    count /= 2;
                    remainder = counts[countIndex] - count;
                    syllable += JoinSyllables(syllables, count, syllableIndex);
                }
                else if (countIndex == counts.Count - 1)
                {
                    remainder = 0;
                    syllable += JoinSyllables(syllables, count, syllableIndex);
                }
                else
                {
                    char previous = syllable[syllable.Length - 1];
                    char next = syllables[syllableIndex + 1][0];
                    string lookup = ";" + previous + syllables[syllableIndex] + next + ";";
                    if (_shortVowels.IndexOf(lookup, StringComparison.OrdinalIgnoreCase) > -1)
                    {
                        remainder = 0;
                        syllable += JoinSyllables(syllables, count, syllableIndex);
                    }
                    else
                    {
                        remainder = 1;
                    }
                }

                syllables.Insert(syllableIndex, syllable);
                syllableIndex++;
                counts.Insert(countIndex, 1);
                countIndex++;
                counts.RemoveAt(countIndex);
                counts.Insert(countIndex, remainder);
            }
        }

        private static string JoinSyllables(List<string> syllables, int count, int syllableIndex)
        {
            string syllable = "";
            for (int index = 0; index < count; index++)
            {
                syllable += syllables[syllableIndex];
                syllables.RemoveAt(syllableIndex);
            }
            return syllable;
        }

        private static void JoinE(List<string> syllables)
        {
            int index = 1;
            while (index < syllables.Count)
            {
                var syllable = syllables[index];
                if (syllable == "e")
                {
                    syllables.RemoveAt(index);
                    string previous = syllables[index - 1];
                    syllables.RemoveAt(index - 1);
                    syllables.Insert(index - 1, previous + syllable);
                }
                else
                {
                    index++;
                }
            }
        }

        #endregion
    }
}