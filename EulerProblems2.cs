using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using java.math;

namespace WindowsApplication1
{
    public partial class Form1 : Form
    {
        double DigitCount(double n)
        {
            return Math.Ceiling(Math.Log(n + 1, 10));
        }
        double ExtractDigit(double n, double i)
        {
            double p = Math.Pow(10, i);
            return Math.Floor(n / p) - 10 * Math.Floor(n / (p * 10));
        }
        void FindDivisors(int num, Dictionary<int, List<int>> memo)
        {
            if (!memo.ContainsKey(num))
            {
                memo.Add(num, new List<int>());
                int mid = (int)Math.Sqrt(num);
                for (int i = 1; i <= mid; ++i)
                {
                    if (num % i == 0)
                    {
                        if (!memo[num].Contains(i))
                            memo[num].Add(i);
                        if (i != 1)
                        {
                            int x = num / i;
                            if (x != i && !memo[num].Contains(x))
                                memo[num].Add(x);
                            FindDivisors(x, memo);
                            foreach (int sub in memo[x])
                                if (!memo[num].Contains(sub))
                                    memo[num].Add(sub);
                        }
                    }
                }
            }
        }
        private List<long> FindPrimes(long underValue, long segmentLength)
        {
            List<long> primes = new List<long>();
            long numSegments = (long)Math.Ceiling((double)underValue / (double)segmentLength);
            for (long k = 0; k < numSegments; ++k)
            {
                long offset = k * segmentLength;
                bool[] isNotPrime = null;
                isNotPrime = new bool[segmentLength];
                if (k == 0)
                {
                    isNotPrime[0] = true;
                    isNotPrime[1] = true;
                    isNotPrime[4] = true;
                }
                else
                {
                    foreach (long prime in primes)
                    {
                        long min = (long)Math.Ceiling((double)offset / (double)prime) * prime;
                        long max = offset + segmentLength;
                        for (long i = min; i < max && i < underValue; i += prime)
                            isNotPrime[i - offset] = true;
                    }
                }
                for (long i = 0; i < isNotPrime.Length; ++i)
                {
                    if (!isNotPrime[i])
                    {
                        long prime = offset + i;
                        for (long j = prime * 2; j < isNotPrime.Length; j += prime)
                            isNotPrime[j - offset] = true;
                    }
                }
                for (long i = 0; i < isNotPrime.Length; ++i)
                    if (!isNotPrime[i])
                        primes.Add(offset + i);
            }
            return primes;
        }
        public long Factorial(long num)
        {
            long output = 1;
            for (long i = 2; i <= num; ++i)
                output *= i;
            return output;
        }
        public Form1()
        {
            InitializeComponent();
        }
        private void show(object i)
        {
            textBox1.Text = i.ToString();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            LoungeProblem();
        }
        double L(double n)
        {
            if (n == 1)
                return 1;
            else
                return L(n - 1) * Math.Pow(10, DigitCount(n)) + n;
        }
        double L2(double n, double i)
        {
            double a, b;
            a = ExtractDigit(n, i);
            b = ExtractDigit(n, DigitCount(n) - i - 1);
            return a - b;
        }
        double L3(double n)
        {
            double result = 0, x = 0;
            double q = DigitCount(n) / 2;
            for (double i = 0; i < q; i += 1)
            {
                x = L2(n, i);
                result += x * x;
            }
            return result;
        }

        void LoungeProblem()
        {
            show(L3(L(5)));
        }
        void problem179()
        {
            int[] counts = new int[10000000];
            for (int i = 0; i < counts.Length; ++i)
                counts[i] = 2;
            for (int x = 2; x < counts.Length; ++x)
                for (int y = x * 2; y < counts.Length; y += x)
                    counts[y]++;
            counts[0] = 0;
            counts[1] = 1;
            int total = 0;
            for (int i = 1; i < counts.Length - 1; ++i)
                if (counts[i] == counts[i + 1])
                    total++;
            show(total);
        }
        void problem41()
        {
            Dictionary<int, List<int>> divisors = new Dictionary<int, List<int>>();
            for (int numDigits = 9; numDigits > 1; --numDigits)
            {
                int max = (int)Factorial(numDigits);
                int k = max;
                List<int> digitStack = new List<int>();
                for (int i = 0; i < max; ++i)
                {
                    int j = 0;
                    for (j = 1; j <= numDigits; ++j)
                        digitStack.Add(j);
                    int m = k;
                    int cur = i;
                    int num = 0;
                    j = numDigits;
                    while (digitStack.Count > 1)
                    {
                        m /= j;
                        j--;
                        int index = cur / m;
                        cur %= m;
                        num *= 10;
                        num += digitStack[index];
                        digitStack.RemoveAt(index);
                    }
                    if (digitStack[0] == 2 || digitStack[0] == 5)
                    {
                        break;
                    }
                    num = num * 10 + digitStack[0];
                    FindDivisors(num, divisors);
                    if (divisors[num].Count == 1)
                    {
                        show(num);
                        return;
                    }
                    if (divisors.Count > 1000000)
                        divisors.Clear(); //proactively save memory
                }
            }
        }
        [Obsolete]
        void problem23()
        {
            Dictionary<int, List<int>> divisors = new Dictionary<int, List<int>>();
            List<int> abundant = new List<int>();
            for (int i = 1; i < 28125; ++i)
            {
                FindDivisors(i, divisors);
                int total = 0;
                foreach (int j in divisors[i])
                    total += j;
                if (total > i)
                    abundant.Add(i);
            }
            int max = abundant[abundant.Count - 1];
            bool[] handled = new bool[max * 2 + 1];
            for (int i = 0; i < abundant.Count; ++i)
            {
                for (int j = i; j < abundant.Count; ++j)
                {
                    int k = abundant[i] + abundant[j];
                    if (!handled[k])
                        handled[k] = true;
                }
            }
            int result = 0;
            for (int i = 0; i < handled.Length; ++i)
                if (!handled[i])
                    result += i;
            show(result);
        }
        [Obsolete]
        void problem142()
        {
            int value = 0;
            show(value);
        }
        void problem63()
        {
            int count = 0;
            int delta = 1;
            int p = 0;
            do
            {
                p++;
                double min = Math.Ceiling(Math.Pow(10, Math.Log(Math.Floor(Math.Pow(10, p - 1)), 10) / p));
                delta = Math.Max(0, (int)(10 - min));
                count += delta;
            } while (delta > 0);
            show(count);
        }
        void problem45()
        {
            ulong a = 285;
            List<ulong> pent = new List<ulong>();
            ulong b = 165;
            pent.Add(40755);

            ulong q = 0;
            while (q == 0)
            {
                a += 2;
                ulong t = a * (a + 1) / 2;
                bool rem = false;
                while (pent.Count > 0 && pent[0] < t)
                {
                    pent.RemoveAt(0);
                    rem = true;
                }
                if (rem && pent.Count == 0)
                {
                    do
                    {
                        b++;
                        pent.Add(b * (3 * b - 1) / 2);
                    } while (pent[pent.Count - 1] < t);
                }
                if (pent.Contains(t))
                    q = t;
            }
            show(q);
        }
        void problem39()
        {
            int maxCount = 0;
            int maxP = 0;
            for (int p = 12; p < 1000; ++p)
            {
                int p2 = p * p;
                int count = 0;
                for (int a = 1; a < p; ++a)
                {
                    int a2 = a * a;
                    int q = p - a;
                    for (int b = 1; b < q; ++b)
                    {
                        int b2 = b * b;
                        int c2 = a2 + b2;
                        int c = p - a - b;
                        if (c2 == c * c)
                        {
                            count++;
                        }
                    }
                }
                if (count > maxCount)
                {
                    maxCount = count;
                    maxP = p;
                }
            }
            show(maxP);
        }
        void problem97()
        {
            long digits = 1;
            for (int i = 0; i < 7830457; ++i)
                digits = (digits * 2) % 10000000000;
            digits *= 28433;
            digits++;
            digits %= 10000000000;
            show(digits);
        }
        void problem35()
        {
            List<long> primes = FindPrimes(1000000, 1000000);
            int count = 0;
            foreach (long prime in primes)
            {
                string s = prime.ToString();
                bool good = true;
                foreach (char c in s)
                {
                    if ((c - '0') % 2 == 0)
                    {
                        good = false;
                    }
                }
                if (good)
                {
                    for (int i = 0; i < s.Length - 1; ++i)
                    {
                        s = s.Substring(1) + s[0].ToString();
                        int k = int.Parse(s);
                        good &= primes.Contains(k);
                    }
                }
                if (good)
                    count++;
            }
            count++; //add one for the prime number 2 which would fail the test
            show(count);
        }
        void problem40()
        {
            StringBuilder d = new StringBuilder();
            int i = 0;
            while (d.Length < 1000000)
            {
                i++;
                d.Append(i);
            }
            int a = d[0] - '0';
            int b = d[9] - '0';
            int c = d[99] - '0';
            int e = d[999] - '0';
            int f = d[9999] - '0';
            int g = d[99999] - '0';
            int h = d[999999] - '0';
            int o = a * b * c * e * f * g * h;
            textBox1.Text = o.ToString();
        }
        void problem36()
        {
            List<int> numbers = new List<int>();
            for (int i = 1; i < 10; i += 2)
                numbers.Add(i);
            for (int numDigits = 2; numDigits <= 4; ++numDigits)
            {
                int T = (int)(Math.Pow(10, numDigits - 1));
                int t = T / 10;
                for (int number = t; number < T; ++number)
                {
                    string x = number.ToString();
                    string y = "";
                    foreach (char c in x)
                        y = c + y;
                    int n2 = int.Parse(y);
                    if (n2 % 2 != 0)
                    {
                        numbers.Add(int.Parse(x + y));
                        for (int mid = 0; mid <= 9; ++mid)
                        {
                            int k = int.Parse(x + mid.ToString() + y);
                            if (k < 1000000)
                                numbers.Add(k);
                        }
                    }
                }
            }
            for (int i = numbers.Count - 1; i >= 0; --i)
            {
                string x = numbers[i].ToString("X");

                x = x.Replace("0", "0000");
                x = x.Replace("1", "0001");
                x = x.Replace("2", "0010");
                x = x.Replace("3", "0011");
                x = x.Replace("4", "0100");
                x = x.Replace("5", "0101");
                x = x.Replace("6", "0110");
                x = x.Replace("7", "0111");
                x = x.Replace("8", "1000");
                x = x.Replace("9", "1001");
                x = x.Replace("A", "1010");
                x = x.Replace("B", "1011");
                x = x.Replace("C", "1100");
                x = x.Replace("D", "1101");
                x = x.Replace("E", "1110");
                x = x.Replace("F", "1111");

                x = x.Substring(x.IndexOf('1'));

                for (int j = 0; j < x.Length / 2; ++j)
                {
                    if (x[j] != x[x.Length - j - 1])
                    {
                        numbers.RemoveAt(i);
                        break;
                    }
                }
            }
            int total = 0;
            foreach (int x in numbers)
                total += x;
            textBox1.Text = total.ToString();
        }
        void problem42()
        {
            string[] words = new string[] { "A", "ABILITY", "ABLE", "ABOUT", "ABOVE", "ABSENCE", "ABSOLUTELY", "ACADEMIC", "ACCEPT", "ACCESS", "ACCIDENT", "ACCOMPANY", "ACCORDING", "ACCOUNT", "ACHIEVE", "ACHIEVEMENT", "ACID", "ACQUIRE", "ACROSS", "ACT", "ACTION", "ACTIVE", "ACTIVITY", "ACTUAL", "ACTUALLY", "ADD", "ADDITION", "ADDITIONAL", "ADDRESS", "ADMINISTRATION", "ADMIT", "ADOPT", "ADULT", "ADVANCE", "ADVANTAGE", "ADVICE", "ADVISE", "AFFAIR", "AFFECT", "AFFORD", "AFRAID", "AFTER", "AFTERNOON", "AFTERWARDS", "AGAIN", "AGAINST", "AGE", "AGENCY", "AGENT", "AGO", "AGREE", "AGREEMENT", "AHEAD", "AID", "AIM", "AIR", "AIRCRAFT", "ALL", "ALLOW", "ALMOST", "ALONE", "ALONG", "ALREADY", "ALRIGHT", "ALSO", "ALTERNATIVE", "ALTHOUGH", "ALWAYS", "AMONG", "AMONGST", "AMOUNT", "AN", "ANALYSIS", "ANCIENT", "AND", "ANIMAL", "ANNOUNCE", "ANNUAL", "ANOTHER", "ANSWER", "ANY", "ANYBODY", "ANYONE", "ANYTHING", "ANYWAY", "APART", "APPARENT", "APPARENTLY", "APPEAL", "APPEAR", "APPEARANCE", "APPLICATION", "APPLY", "APPOINT", "APPOINTMENT", "APPROACH", "APPROPRIATE", "APPROVE", "AREA", "ARGUE", "ARGUMENT", "ARISE", "ARM", "ARMY", "AROUND", "ARRANGE", "ARRANGEMENT", "ARRIVE", "ART", "ARTICLE", "ARTIST", "AS", "ASK", "ASPECT", "ASSEMBLY", "ASSESS", "ASSESSMENT", "ASSET", "ASSOCIATE", "ASSOCIATION", "ASSUME", "ASSUMPTION", "AT", "ATMOSPHERE", "ATTACH", "ATTACK", "ATTEMPT", "ATTEND", "ATTENTION", "ATTITUDE", "ATTRACT", "ATTRACTIVE", "AUDIENCE", "AUTHOR", "AUTHORITY", "AVAILABLE", "AVERAGE", "AVOID", "AWARD", "AWARE", "AWAY", "AYE", "BABY", "BACK", "BACKGROUND", "BAD", "BAG", "BALANCE", "BALL", "BAND", "BANK", "BAR", "BASE", "BASIC", "BASIS", "BATTLE", "BE", "BEAR", "BEAT", "BEAUTIFUL", "BECAUSE", "BECOME", "BED", "BEDROOM", "BEFORE", "BEGIN", "BEGINNING", "BEHAVIOUR", "BEHIND", "BELIEF", "BELIEVE", "BELONG", "BELOW", "BENEATH", "BENEFIT", "BESIDE", "BEST", "BETTER", "BETWEEN", "BEYOND", "BIG", "BILL", "BIND", "BIRD", "BIRTH", "BIT", "BLACK", "BLOCK", "BLOOD", "BLOODY", "BLOW", "BLUE", "BOARD", "BOAT", "BODY", "BONE", "BOOK", "BORDER", "BOTH", "BOTTLE", "BOTTOM", "BOX", "BOY", "BRAIN", "BRANCH", "BREAK", "BREATH", "BRIDGE", "BRIEF", "BRIGHT", "BRING", "BROAD", "BROTHER", "BUDGET", "BUILD", "BUILDING", "BURN", "BUS", "BUSINESS", "BUSY", "BUT", "BUY", "BY", "CABINET", "CALL", "CAMPAIGN", "CAN", "CANDIDATE", "CAPABLE", "CAPACITY", "CAPITAL", "CAR", "CARD", "CARE", "CAREER", "CAREFUL", "CAREFULLY", "CARRY", "CASE", "CASH", "CAT", "CATCH", "CATEGORY", "CAUSE", "CELL", "CENTRAL", "CENTRE", "CENTURY", "CERTAIN", "CERTAINLY", "CHAIN", "CHAIR", "CHAIRMAN", "CHALLENGE", "CHANCE", "CHANGE", "CHANNEL", "CHAPTER", "CHARACTER", "CHARACTERISTIC", "CHARGE", "CHEAP", "CHECK", "CHEMICAL", "CHIEF", "CHILD", "CHOICE", "CHOOSE", "CHURCH", "CIRCLE", "CIRCUMSTANCE", "CITIZEN", "CITY", "CIVIL", "CLAIM", "CLASS", "CLEAN", "CLEAR", "CLEARLY", "CLIENT", "CLIMB", "CLOSE", "CLOSELY", "CLOTHES", "CLUB", "COAL", "CODE", "COFFEE", "COLD", "COLLEAGUE", "COLLECT", "COLLECTION", "COLLEGE", "COLOUR", "COMBINATION", "COMBINE", "COME", "COMMENT", "COMMERCIAL", "COMMISSION", "COMMIT", "COMMITMENT", "COMMITTEE", "COMMON", "COMMUNICATION", "COMMUNITY", "COMPANY", "COMPARE", "COMPARISON", "COMPETITION", "COMPLETE", "COMPLETELY", "COMPLEX", "COMPONENT", "COMPUTER", "CONCENTRATE", "CONCENTRATION", "CONCEPT", "CONCERN", "CONCERNED", "CONCLUDE", "CONCLUSION", "CONDITION", "CONDUCT", "CONFERENCE", "CONFIDENCE", "CONFIRM", "CONFLICT", "CONGRESS", "CONNECT", "CONNECTION", "CONSEQUENCE", "CONSERVATIVE", "CONSIDER", "CONSIDERABLE", "CONSIDERATION", "CONSIST", "CONSTANT", "CONSTRUCTION", "CONSUMER", "CONTACT", "CONTAIN", "CONTENT", "CONTEXT", "CONTINUE", "CONTRACT", "CONTRAST", "CONTRIBUTE", "CONTRIBUTION", "CONTROL", "CONVENTION", "CONVERSATION", "COPY", "CORNER", "CORPORATE", "CORRECT", "COS", "COST", "COULD", "COUNCIL", "COUNT", "COUNTRY", "COUNTY", "COUPLE", "COURSE", "COURT", "COVER", "CREATE", "CREATION", "CREDIT", "CRIME", "CRIMINAL", "CRISIS", "CRITERION", "CRITICAL", "CRITICISM", "CROSS", "CROWD", "CRY", "CULTURAL", "CULTURE", "CUP", "CURRENT", "CURRENTLY", "CURRICULUM", "CUSTOMER", "CUT", "DAMAGE", "DANGER", "DANGEROUS", "DARK", "DATA", "DATE", "DAUGHTER", "DAY", "DEAD", "DEAL", "DEATH", "DEBATE", "DEBT", "DECADE", "DECIDE", "DECISION", "DECLARE", "DEEP", "DEFENCE", "DEFENDANT", "DEFINE", "DEFINITION", "DEGREE", "DELIVER", "DEMAND", "DEMOCRATIC", "DEMONSTRATE", "DENY", "DEPARTMENT", "DEPEND", "DEPUTY", "DERIVE", "DESCRIBE", "DESCRIPTION", "DESIGN", "DESIRE", "DESK", "DESPITE", "DESTROY", "DETAIL", "DETAILED", "DETERMINE", "DEVELOP", "DEVELOPMENT", "DEVICE", "DIE", "DIFFERENCE", "DIFFERENT", "DIFFICULT", "DIFFICULTY", "DINNER", "DIRECT", "DIRECTION", "DIRECTLY", "DIRECTOR", "DISAPPEAR", "DISCIPLINE", "DISCOVER", "DISCUSS", "DISCUSSION", "DISEASE", "DISPLAY", "DISTANCE", "DISTINCTION", "DISTRIBUTION", "DISTRICT", "DIVIDE", "DIVISION", "DO", "DOCTOR", "DOCUMENT", "DOG", "DOMESTIC", "DOOR", "DOUBLE", "DOUBT", "DOWN", "DRAW", "DRAWING", "DREAM", "DRESS", "DRINK", "DRIVE", "DRIVER", "DROP", "DRUG", "DRY", "DUE", "DURING", "DUTY", "EACH", "EAR", "EARLY", "EARN", "EARTH", "EASILY", "EAST", "EASY", "EAT", "ECONOMIC", "ECONOMY", "EDGE", "EDITOR", "EDUCATION", "EDUCATIONAL", "EFFECT", "EFFECTIVE", "EFFECTIVELY", "EFFORT", "EGG", "EITHER", "ELDERLY", "ELECTION", "ELEMENT", "ELSE", "ELSEWHERE", "EMERGE", "EMPHASIS", "EMPLOY", "EMPLOYEE", "EMPLOYER", "EMPLOYMENT", "EMPTY", "ENABLE", "ENCOURAGE", "END", "ENEMY", "ENERGY", "ENGINE", "ENGINEERING", "ENJOY", "ENOUGH", "ENSURE", "ENTER", "ENTERPRISE", "ENTIRE", "ENTIRELY", "ENTITLE", "ENTRY", "ENVIRONMENT", "ENVIRONMENTAL", "EQUAL", "EQUALLY", "EQUIPMENT", "ERROR", "ESCAPE", "ESPECIALLY", "ESSENTIAL", "ESTABLISH", "ESTABLISHMENT", "ESTATE", "ESTIMATE", "EVEN", "EVENING", "EVENT", "EVENTUALLY", "EVER", "EVERY", "EVERYBODY", "EVERYONE", "EVERYTHING", "EVIDENCE", "EXACTLY", "EXAMINATION", "EXAMINE", "EXAMPLE", "EXCELLENT", "EXCEPT", "EXCHANGE", "EXECUTIVE", "EXERCISE", "EXHIBITION", "EXIST", "EXISTENCE", "EXISTING", "EXPECT", "EXPECTATION", "EXPENDITURE", "EXPENSE", "EXPENSIVE", "EXPERIENCE", "EXPERIMENT", "EXPERT", "EXPLAIN", "EXPLANATION", "EXPLORE", "EXPRESS", "EXPRESSION", "EXTEND", "EXTENT", "EXTERNAL", "EXTRA", "EXTREMELY", "EYE", "FACE", "FACILITY", "FACT", "FACTOR", "FACTORY", "FAIL", "FAILURE", "FAIR", "FAIRLY", "FAITH", "FALL", "FAMILIAR", "FAMILY", "FAMOUS", "FAR", "FARM", "FARMER", "FASHION", "FAST", "FATHER", "FAVOUR", "FEAR", "FEATURE", "FEE", "FEEL", "FEELING", "FEMALE", "FEW", "FIELD", "FIGHT", "FIGURE", "FILE", "FILL", "FILM", "FINAL", "FINALLY", "FINANCE", "FINANCIAL", "FIND", "FINDING", "FINE", "FINGER", "FINISH", "FIRE", "FIRM", "FIRST", "FISH", "FIT", "FIX", "FLAT", "FLIGHT", "FLOOR", "FLOW", "FLOWER", "FLY", "FOCUS", "FOLLOW", "FOLLOWING", "FOOD", "FOOT", "FOOTBALL", "FOR", "FORCE", "FOREIGN", "FOREST", "FORGET", "FORM", "FORMAL", "FORMER", "FORWARD", "FOUNDATION", "FREE", "FREEDOM", "FREQUENTLY", "FRESH", "FRIEND", "FROM", "FRONT", "FRUIT", "FUEL", "FULL", "FULLY", "FUNCTION", "FUND", "FUNNY", "FURTHER", "FUTURE", "GAIN", "GAME", "GARDEN", "GAS", "GATE", "GATHER", "GENERAL", "GENERALLY", "GENERATE", "GENERATION", "GENTLEMAN", "GET", "GIRL", "GIVE", "GLASS", "GO", "GOAL", "GOD", "GOLD", "GOOD", "GOVERNMENT", "GRANT", "GREAT", "GREEN", "GREY", "GROUND", "GROUP", "GROW", "GROWING", "GROWTH", "GUEST", "GUIDE", "GUN", "HAIR", "HALF", "HALL", "HAND", "HANDLE", "HANG", "HAPPEN", "HAPPY", "HARD", "HARDLY", "HATE", "HAVE", "HE", "HEAD", "HEALTH", "HEAR", "HEART", "HEAT", "HEAVY", "HELL", "HELP", "HENCE", "HER", "HERE", "HERSELF", "HIDE", "HIGH", "HIGHLY", "HILL", "HIM", "HIMSELF", "HIS", "HISTORICAL", "HISTORY", "HIT", "HOLD", "HOLE", "HOLIDAY", "HOME", "HOPE", "HORSE", "HOSPITAL", "HOT", "HOTEL", "HOUR", "HOUSE", "HOUSEHOLD", "HOUSING", "HOW", "HOWEVER", "HUGE", "HUMAN", "HURT", "HUSBAND", "I", "IDEA", "IDENTIFY", "IF", "IGNORE", "ILLUSTRATE", "IMAGE", "IMAGINE", "IMMEDIATE", "IMMEDIATELY", "IMPACT", "IMPLICATION", "IMPLY", "IMPORTANCE", "IMPORTANT", "IMPOSE", "IMPOSSIBLE", "IMPRESSION", "IMPROVE", "IMPROVEMENT", "IN", "INCIDENT", "INCLUDE", "INCLUDING", "INCOME", "INCREASE", "INCREASED", "INCREASINGLY", "INDEED", "INDEPENDENT", "INDEX", "INDICATE", "INDIVIDUAL", "INDUSTRIAL", "INDUSTRY", "INFLUENCE", "INFORM", "INFORMATION", "INITIAL", "INITIATIVE", "INJURY", "INSIDE", "INSIST", "INSTANCE", "INSTEAD", "INSTITUTE", "INSTITUTION", "INSTRUCTION", "INSTRUMENT", "INSURANCE", "INTEND", "INTENTION", "INTEREST", "INTERESTED", "INTERESTING", "INTERNAL", "INTERNATIONAL", "INTERPRETATION", "INTERVIEW", "INTO", "INTRODUCE", "INTRODUCTION", "INVESTIGATE", "INVESTIGATION", "INVESTMENT", "INVITE", "INVOLVE", "IRON", "IS", "ISLAND", "ISSUE", "IT", "ITEM", "ITS", "ITSELF", "JOB", "JOIN", "JOINT", "JOURNEY", "JUDGE", "JUMP", "JUST", "JUSTICE", "KEEP", "KEY", "KID", "KILL", "KIND", "KING", "KITCHEN", "KNEE", "KNOW", "KNOWLEDGE", "LABOUR", "LACK", "LADY", "LAND", "LANGUAGE", "LARGE", "LARGELY", "LAST", "LATE", "LATER", "LATTER", "LAUGH", "LAUNCH", "LAW", "LAWYER", "LAY", "LEAD", "LEADER", "LEADERSHIP", "LEADING", "LEAF", "LEAGUE", "LEAN", "LEARN", "LEAST", "LEAVE", "LEFT", "LEG", "LEGAL", "LEGISLATION", "LENGTH", "LESS", "LET", "LETTER", "LEVEL", "LIABILITY", "LIBERAL", "LIBRARY", "LIE", "LIFE", "LIFT", "LIGHT", "LIKE", "LIKELY", "LIMIT", "LIMITED", "LINE", "LINK", "LIP", "LIST", "LISTEN", "LITERATURE", "LITTLE", "LIVE", "LIVING", "LOAN", "LOCAL", "LOCATION", "LONG", "LOOK", "LORD", "LOSE", "LOSS", "LOT", "LOVE", "LOVELY", "LOW", "LUNCH", "MACHINE", "MAGAZINE", "MAIN", "MAINLY", "MAINTAIN", "MAJOR", "MAJORITY", "MAKE", "MALE", "MAN", "MANAGE", "MANAGEMENT", "MANAGER", "MANNER", "MANY", "MAP", "MARK", "MARKET", "MARRIAGE", "MARRIED", "MARRY", "MASS", "MASTER", "MATCH", "MATERIAL", "MATTER", "MAY", "MAYBE", "ME", "MEAL", "MEAN", "MEANING", "MEANS", "MEANWHILE", "MEASURE", "MECHANISM", "MEDIA", "MEDICAL", "MEET", "MEETING", "MEMBER", "MEMBERSHIP", "MEMORY", "MENTAL", "MENTION", "MERELY", "MESSAGE", "METAL", "METHOD", "MIDDLE", "MIGHT", "MILE", "MILITARY", "MILK", "MIND", "MINE", "MINISTER", "MINISTRY", "MINUTE", "MISS", "MISTAKE", "MODEL", "MODERN", "MODULE", "MOMENT", "MONEY", "MONTH", "MORE", "MORNING", "MOST", "MOTHER", "MOTION", "MOTOR", "MOUNTAIN", "MOUTH", "MOVE", "MOVEMENT", "MUCH", "MURDER", "MUSEUM", "MUSIC", "MUST", "MY", "MYSELF", "NAME", "NARROW", "NATION", "NATIONAL", "NATURAL", "NATURE", "NEAR", "NEARLY", "NECESSARILY", "NECESSARY", "NECK", "NEED", "NEGOTIATION", "NEIGHBOUR", "NEITHER", "NETWORK", "NEVER", "NEVERTHELESS", "NEW", "NEWS", "NEWSPAPER", "NEXT", "NICE", "NIGHT", "NO", "NOBODY", "NOD", "NOISE", "NONE", "NOR", "NORMAL", "NORMALLY", "NORTH", "NORTHERN", "NOSE", "NOT", "NOTE", "NOTHING", "NOTICE", "NOTION", "NOW", "NUCLEAR", "NUMBER", "NURSE", "OBJECT", "OBJECTIVE", "OBSERVATION", "OBSERVE", "OBTAIN", "OBVIOUS", "OBVIOUSLY", "OCCASION", "OCCUR", "ODD", "OF", "OFF", "OFFENCE", "OFFER", "OFFICE", "OFFICER", "OFFICIAL", "OFTEN", "OIL", "OKAY", "OLD", "ON", "ONCE", "ONE", "ONLY", "ONTO", "OPEN", "OPERATE", "OPERATION", "OPINION", "OPPORTUNITY", "OPPOSITION", "OPTION", "OR", "ORDER", "ORDINARY", "ORGANISATION", "ORGANISE", "ORGANIZATION", "ORIGIN", "ORIGINAL", "OTHER", "OTHERWISE", "OUGHT", "OUR", "OURSELVES", "OUT", "OUTCOME", "OUTPUT", "OUTSIDE", "OVER", "OVERALL", "OWN", "OWNER", "PACKAGE", "PAGE", "PAIN", "PAINT", "PAINTING", "PAIR", "PANEL", "PAPER", "PARENT", "PARK", "PARLIAMENT", "PART", "PARTICULAR", "PARTICULARLY", "PARTLY", "PARTNER", "PARTY", "PASS", "PASSAGE", "PAST", "PATH", "PATIENT", "PATTERN", "PAY", "PAYMENT", "PEACE", "PENSION", "PEOPLE", "PER", "PERCENT", "PERFECT", "PERFORM", "PERFORMANCE", "PERHAPS", "PERIOD", "PERMANENT", "PERSON", "PERSONAL", "PERSUADE", "PHASE", "PHONE", "PHOTOGRAPH", "PHYSICAL", "PICK", "PICTURE", "PIECE", "PLACE", "PLAN", "PLANNING", "PLANT", "PLASTIC", "PLATE", "PLAY", "PLAYER", "PLEASE", "PLEASURE", "PLENTY", "PLUS", "POCKET", "POINT", "POLICE", "POLICY", "POLITICAL", "POLITICS", "POOL", "POOR", "POPULAR", "POPULATION", "POSITION", "POSITIVE", "POSSIBILITY", "POSSIBLE", "POSSIBLY", "POST", "POTENTIAL", "POUND", "POWER", "POWERFUL", "PRACTICAL", "PRACTICE", "PREFER", "PREPARE", "PRESENCE", "PRESENT", "PRESIDENT", "PRESS", "PRESSURE", "PRETTY", "PREVENT", "PREVIOUS", "PREVIOUSLY", "PRICE", "PRIMARY", "PRIME", "PRINCIPLE", "PRIORITY", "PRISON", "PRISONER", "PRIVATE", "PROBABLY", "PROBLEM", "PROCEDURE", "PROCESS", "PRODUCE", "PRODUCT", "PRODUCTION", "PROFESSIONAL", "PROFIT", "PROGRAM", "PROGRAMME", "PROGRESS", "PROJECT", "PROMISE", "PROMOTE", "PROPER", "PROPERLY", "PROPERTY", "PROPORTION", "PROPOSE", "PROPOSAL", "PROSPECT", "PROTECT", "PROTECTION", "PROVE", "PROVIDE", "PROVIDED", "PROVISION", "PUB", "PUBLIC", "PUBLICATION", "PUBLISH", "PULL", "PUPIL", "PURPOSE", "PUSH", "PUT", "QUALITY", "QUARTER", "QUESTION", "QUICK", "QUICKLY", "QUIET", "QUITE", "RACE", "RADIO", "RAILWAY", "RAIN", "RAISE", "RANGE", "RAPIDLY", "RARE", "RATE", "RATHER", "REACH", "REACTION", "READ", "READER", "READING", "READY", "REAL", "REALISE", "REALITY", "REALIZE", "REALLY", "REASON", "REASONABLE", "RECALL", "RECEIVE", "RECENT", "RECENTLY", "RECOGNISE", "RECOGNITION", "RECOGNIZE", "RECOMMEND", "RECORD", "RECOVER", "RED", "REDUCE", "REDUCTION", "REFER", "REFERENCE", "REFLECT", "REFORM", "REFUSE", "REGARD", "REGION", "REGIONAL", "REGULAR", "REGULATION", "REJECT", "RELATE", "RELATION", "RELATIONSHIP", "RELATIVE", "RELATIVELY", "RELEASE", "RELEVANT", "RELIEF", "RELIGION", "RELIGIOUS", "RELY", "REMAIN", "REMEMBER", "REMIND", "REMOVE", "REPEAT", "REPLACE", "REPLY", "REPORT", "REPRESENT", "REPRESENTATION", "REPRESENTATIVE", "REQUEST", "REQUIRE", "REQUIREMENT", "RESEARCH", "RESOURCE", "RESPECT", "RESPOND", "RESPONSE", "RESPONSIBILITY", "RESPONSIBLE", "REST", "RESTAURANT", "RESULT", "RETAIN", "RETURN", "REVEAL", "REVENUE", "REVIEW", "REVOLUTION", "RICH", "RIDE", "RIGHT", "RING", "RISE", "RISK", "RIVER", "ROAD", "ROCK", "ROLE", "ROLL", "ROOF", "ROOM", "ROUND", "ROUTE", "ROW", "ROYAL", "RULE", "RUN", "RURAL", "SAFE", "SAFETY", "SALE", "SAME", "SAMPLE", "SATISFY", "SAVE", "SAY", "SCALE", "SCENE", "SCHEME", "SCHOOL", "SCIENCE", "SCIENTIFIC", "SCIENTIST", "SCORE", "SCREEN", "SEA", "SEARCH", "SEASON", "SEAT", "SECOND", "SECONDARY", "SECRETARY", "SECTION", "SECTOR", "SECURE", "SECURITY", "SEE", "SEEK", "SEEM", "SELECT", "SELECTION", "SELL", "SEND", "SENIOR", "SENSE", "SENTENCE", "SEPARATE", "SEQUENCE", "SERIES", "SERIOUS", "SERIOUSLY", "SERVANT", "SERVE", "SERVICE", "SESSION", "SET", "SETTLE", "SETTLEMENT", "SEVERAL", "SEVERE", "SEX", "SEXUAL", "SHAKE", "SHALL", "SHAPE", "SHARE", "SHE", "SHEET", "SHIP", "SHOE", "SHOOT", "SHOP", "SHORT", "SHOT", "SHOULD", "SHOULDER", "SHOUT", "SHOW", "SHUT", "SIDE", "SIGHT", "SIGN", "SIGNAL", "SIGNIFICANCE", "SIGNIFICANT", "SILENCE", "SIMILAR", "SIMPLE", "SIMPLY", "SINCE", "SING", "SINGLE", "SIR", "SISTER", "SIT", "SITE", "SITUATION", "SIZE", "SKILL", "SKIN", "SKY", "SLEEP", "SLIGHTLY", "SLIP", "SLOW", "SLOWLY", "SMALL", "SMILE", "SO", "SOCIAL", "SOCIETY", "SOFT", "SOFTWARE", "SOIL", "SOLDIER", "SOLICITOR", "SOLUTION", "SOME", "SOMEBODY", "SOMEONE", "SOMETHING", "SOMETIMES", "SOMEWHAT", "SOMEWHERE", "SON", "SONG", "SOON", "SORRY", "SORT", "SOUND", "SOURCE", "SOUTH", "SOUTHERN", "SPACE", "SPEAK", "SPEAKER", "SPECIAL", "SPECIES", "SPECIFIC", "SPEECH", "SPEED", "SPEND", "SPIRIT", "SPORT", "SPOT", "SPREAD", "SPRING", "STAFF", "STAGE", "STAND", "STANDARD", "STAR", "START", "STATE", "STATEMENT", "STATION", "STATUS", "STAY", "STEAL", "STEP", "STICK", "STILL", "STOCK", "STONE", "STOP", "STORE", "STORY", "STRAIGHT", "STRANGE", "STRATEGY", "STREET", "STRENGTH", "STRIKE", "STRONG", "STRONGLY", "STRUCTURE", "STUDENT", "STUDIO", "STUDY", "STUFF", "STYLE", "SUBJECT", "SUBSTANTIAL", "SUCCEED", "SUCCESS", "SUCCESSFUL", "SUCH", "SUDDENLY", "SUFFER", "SUFFICIENT", "SUGGEST", "SUGGESTION", "SUITABLE", "SUM", "SUMMER", "SUN", "SUPPLY", "SUPPORT", "SUPPOSE", "SURE", "SURELY", "SURFACE", "SURPRISE", "SURROUND", "SURVEY", "SURVIVE", "SWITCH", "SYSTEM", "TABLE", "TAKE", "TALK", "TALL", "TAPE", "TARGET", "TASK", "TAX", "TEA", "TEACH", "TEACHER", "TEACHING", "TEAM", "TEAR", "TECHNICAL", "TECHNIQUE", "TECHNOLOGY", "TELEPHONE", "TELEVISION", "TELL", "TEMPERATURE", "TEND", "TERM", "TERMS", "TERRIBLE", "TEST", "TEXT", "THAN", "THANK", "THANKS", "THAT", "THE", "THEATRE", "THEIR", "THEM", "THEME", "THEMSELVES", "THEN", "THEORY", "THERE", "THEREFORE", "THESE", "THEY", "THIN", "THING", "THINK", "THIS", "THOSE", "THOUGH", "THOUGHT", "THREAT", "THREATEN", "THROUGH", "THROUGHOUT", "THROW", "THUS", "TICKET", "TIME", "TINY", "TITLE", "TO", "TODAY", "TOGETHER", "TOMORROW", "TONE", "TONIGHT", "TOO", "TOOL", "TOOTH", "TOP", "TOTAL", "TOTALLY", "TOUCH", "TOUR", "TOWARDS", "TOWN", "TRACK", "TRADE", "TRADITION", "TRADITIONAL", "TRAFFIC", "TRAIN", "TRAINING", "TRANSFER", "TRANSPORT", "TRAVEL", "TREAT", "TREATMENT", "TREATY", "TREE", "TREND", "TRIAL", "TRIP", "TROOP", "TROUBLE", "TRUE", "TRUST", "TRUTH", "TRY", "TURN", "TWICE", "TYPE", "TYPICAL", "UNABLE", "UNDER", "UNDERSTAND", "UNDERSTANDING", "UNDERTAKE", "UNEMPLOYMENT", "UNFORTUNATELY", "UNION", "UNIT", "UNITED", "UNIVERSITY", "UNLESS", "UNLIKELY", "UNTIL", "UP", "UPON", "UPPER", "URBAN", "US", "USE", "USED", "USEFUL", "USER", "USUAL", "USUALLY", "VALUE", "VARIATION", "VARIETY", "VARIOUS", "VARY", "VAST", "VEHICLE", "VERSION", "VERY", "VIA", "VICTIM", "VICTORY", "VIDEO", "VIEW", "VILLAGE", "VIOLENCE", "VISION", "VISIT", "VISITOR", "VITAL", "VOICE", "VOLUME", "VOTE", "WAGE", "WAIT", "WALK", "WALL", "WANT", "WAR", "WARM", "WARN", "WASH", "WATCH", "WATER", "WAVE", "WAY", "WE", "WEAK", "WEAPON", "WEAR", "WEATHER", "WEEK", "WEEKEND", "WEIGHT", "WELCOME", "WELFARE", "WELL", "WEST", "WESTERN", "WHAT", "WHATEVER", "WHEN", "WHERE", "WHEREAS", "WHETHER", "WHICH", "WHILE", "WHILST", "WHITE", "WHO", "WHOLE", "WHOM", "WHOSE", "WHY", "WIDE", "WIDELY", "WIFE", "WILD", "WILL", "WIN", "WIND", "WINDOW", "WINE", "WING", "WINNER", "WINTER", "WISH", "WITH", "WITHDRAW", "WITHIN", "WITHOUT", "WOMAN", "WONDER", "WONDERFUL", "WOOD", "WORD", "WORK", "WORKER", "WORKING", "WORKS", "WORLD", "WORRY", "WORTH", "WOULD", "WRITE", "WRITER", "WRITING", "WRONG", "YARD", "YEAH", "YEAR", "YES", "YESTERDAY", "YET", "YOU", "YOUNG", "YOUR", "YOURSELF", "YOUTH" };
            int[] values = new int[words.Length];
            int max = 0;
            for (int i = 0; i < words.Length; ++i)
            {
                int total = 0;
                foreach (char c in words[i]) total += (int)(c - 'A') + 1;
                values[i] = total;
                if (total > max) max = total;
            }
            List<int> triangles = new List<int>();
            int cur = 1;
            int val = 1;
            while (val <= max)
            {
                triangles.Add(val);
                cur++;
                val += cur;
            }
            int count = 0;
            foreach (int p in values)
                if (triangles.Contains(p))
                    count++;
            textBox1.Text = count.ToString();
        }
        void problem30()
        {
            List<int> digits = new List<int>();
            for (int i = 0; i < 10; ++i) digits.Add((int)Math.Pow(i, 5));
            int total = 0;
            for (int i = digits[2]; i < 10000000; ++i)
            {
                string sel = i.ToString();
                int num = 0;
                foreach (char c in sel) num += digits[(int)(c - '0')];
                if (i == num)
                    total += num;
            }
            textBox1.Text = total.ToString();
        }
        void problem24()
        {
            List<int> numbers = new List<int>();
            for (int i = 0; i < 10; ++i) numbers.Add(i);
            int radix = 3628800; //9!

            string output = "";
            int num = 999999;
            for (int i = 10; i >= 1; --i)
            {
                radix /= i;
                int digit = numbers[num / radix];
                numbers.Remove(digit);
                output += digit.ToString();
                num = num % radix;
            }
            this.textBox1.Text = output;
        }
        void problem17()
        {
            int total = 0;
            for (int i = 1; i <= 1000; ++i)
            {
                string word = MakeWord(i);

                total += word.Length;
            }
            this.textBox1.Text = total.ToString();
        }
        string MakeWord(int i)
        {
            string output = "";
            if (i == 1000)
            {
                output = "onethousand";
            }
            else
            {
                if (i >= 100)
                {
                    output += MakeWord(i / 100) + "hundred";
                    i %= 100;
                    if (i > 0)
                        output += "and";
                }
                if (i >= 20)
                {
                    int t = i / 10;
                    switch (t)
                    {
                        case 2: output += "twenty"; break;
                        case 3: output += "thirty"; break;
                        case 4: output += "forty"; break;
                        case 5: output += "fifty"; break;
                        case 6: output += "sixty"; break;
                        case 7: output += "seventy"; break;
                        case 8: output += "eighty"; break;
                        case 9: output += "ninety"; break;
                    }
                    i %= 10;
                }
                switch (i)
                {
                    case 19: output += "nineteen"; break;
                    case 18: output += "eighteen"; break;
                    case 17: output += "seventeen"; break;
                    case 16: output += "sixteen"; break;
                    case 15: output += "fifteen"; break;
                    case 14: output += "fourteen"; break;
                    case 13: output += "thirteen"; break;
                    case 12: output += "twelve"; break;
                    case 11: output += "eleven"; break;
                    case 10: output += "ten"; break;
                    case 9: output += "nine"; break;
                    case 8: output += "eight"; break;
                    case 7: output += "seven"; break;
                    case 6: output += "six"; break;
                    case 5: output += "five"; break;
                    case 4: output += "four"; break;
                    case 3: output += "three"; break;
                    case 2: output += "two"; break;
                    case 1: output += "one"; break;
                }
            }
            return output;
        }
        void problem14()
        {
            Dictionary<long, int> lengths = new Dictionary<long, int>();
            long max = long.MinValue;
            int maxLen = 0;
            for (long i = 0; i < 1000000; ++i)
            {
                int length = Collatz(i, lengths);
                if (length > maxLen)
                {
                    maxLen = length;
                    max = i;
                }
            }
            textBox1.Text = string.Format("{0} : {1}", max, maxLen);
        }
        int Collatz(long num, Dictionary<long, int> lengths)
        {
            int total = 1;
            long cur = num;
            while (cur > 1)
            {
                if (lengths.ContainsKey(cur))
                {
                    total += lengths[cur];
                    cur = 1;
                }
                else
                {
                    total++;
                    if (cur % 2 == 0) cur = cur / 2;
                    else cur = cur * 3 + 1;
                }
            }
            if (!lengths.ContainsKey(num))
                lengths.Add(num, total);
            return total;
        }
    }
}