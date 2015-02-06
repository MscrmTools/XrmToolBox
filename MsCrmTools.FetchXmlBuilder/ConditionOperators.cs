using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MsCrmTools.FetchXmlBuilder
{
    internal class Operator
    {
        public string Label { get; set; }
        public string Value { get; set; }
        public ValidityType TypeValidity { get; set; }

        [Flags]
        public enum ValidityType
        {
            ValidForText = 0x1,
            ValidForNumber = 0x2,
            ValidForDate = 0x4,
            ValidForUser = 0x8,
            ValidForBusinessUnit = 0x10,
            ValidForLookup = 0x20,
            ValidForPicklist = 0x40,
            ValidForTeam = 0x80
        }
    }

    internal class ConditionOperators
    {
        private List<Operator> Operators = new List<Operator>
                                               {
                                                   new Operator
                                                       {
                                                           Label = "Equal",
                                                           Value = "eq",
                                                           TypeValidity =
                                                               Operator.ValidityType.ValidForBusinessUnit |
                                                               Operator.ValidityType.ValidForTeam |
                                                               Operator.ValidityType.ValidForDate |
                                                               Operator.ValidityType.ValidForLookup |
                                                               Operator.ValidityType.ValidForNumber |
                                                               Operator.ValidityType.ValidForText |
                                                               Operator.ValidityType.ValidForUser | Operator.ValidityType.ValidForPicklist
                                                       },
                                                   new Operator {Label = "Not Equal", Value = "neq",
                                                           TypeValidity =
                                                               Operator.ValidityType.ValidForBusinessUnit |
                                                               Operator.ValidityType.ValidForTeam |
                                                               Operator.ValidityType.ValidForDate |
                                                               Operator.ValidityType.ValidForLookup |
                                                               Operator.ValidityType.ValidForNumber |
                                                               Operator.ValidityType.ValidForText |
                                                               Operator.ValidityType.ValidForUser | Operator.ValidityType.ValidForPicklist},
                                                   new Operator {Label = "Not Equal (2)", Value = "ne",
                                                           TypeValidity =
                                                               Operator.ValidityType.ValidForBusinessUnit |
                                                               Operator.ValidityType.ValidForTeam |
                                                               Operator.ValidityType.ValidForDate |
                                                               Operator.ValidityType.ValidForLookup |
                                                               Operator.ValidityType.ValidForNumber |
                                                               Operator.ValidityType.ValidForText |
                                                               Operator.ValidityType.ValidForUser | Operator.ValidityType.ValidForPicklist},
                                                   new Operator {Label = "Greater than", Value = "gt", TypeValidity = Operator.ValidityType.ValidForNumber},
                                                   new Operator {Label = "Greater or equal", Value = "ge", TypeValidity = Operator.ValidityType.ValidForNumber},
                                                   new Operator {Label = "Lower or equal", Value = "le", TypeValidity = Operator.ValidityType.ValidForNumber},
                                                   new Operator {Label = "Lower than", Value = "lt", TypeValidity = Operator.ValidityType.ValidForNumber},
                                                   new Operator {Label = "Like", Value = "like", TypeValidity = Operator.ValidityType.ValidForText},
                                                   new Operator {Label = "Not like", Value = "not-like", TypeValidity = Operator.ValidityType.ValidForText},
                                                   new Operator {Label = "In", Value = "in", TypeValidity = Operator.ValidityType.ValidForUser | Operator.ValidityType.ValidForPicklist | Operator.ValidityType.ValidForLookup},
                                                   new Operator {Label = "Not in", Value = "not-in", TypeValidity = Operator.ValidityType.ValidForUser | Operator.ValidityType.ValidForPicklist | Operator.ValidityType.ValidForLookup},
                                                   new Operator {Label = "Between", Value = "between", TypeValidity = Operator.ValidityType.ValidForNumber | Operator.ValidityType.ValidForDate},
                                                   new Operator {Label = "Not between", Value = "not-between", TypeValidity = Operator.ValidityType.ValidForNumber | Operator.ValidityType.ValidForDate},
                                                   new Operator {Label = "Null", Value = "null", TypeValidity =
                                                               Operator.ValidityType.ValidForBusinessUnit |
                                                               Operator.ValidityType.ValidForDate |
                                                               Operator.ValidityType.ValidForLookup |
                                                               Operator.ValidityType.ValidForNumber | 
                                                               Operator.ValidityType.ValidForText |
                                                               Operator.ValidityType.ValidForUser},
                                                   new Operator {Label = "Not null", Value = "not-null", TypeValidity =
                                                               Operator.ValidityType.ValidForBusinessUnit |
                                                               Operator.ValidityType.ValidForDate |
                                                               Operator.ValidityType.ValidForLookup |
                                                               Operator.ValidityType.ValidForNumber |
                                                               Operator.ValidityType.ValidForText |
                                                               Operator.ValidityType.ValidForUser},
                                                   new Operator {Label = "Yesterday", Value = "yesterday", TypeValidity = Operator.ValidityType.ValidForDate},
                                                   new Operator {Label = "Today", Value = "today", TypeValidity = Operator.ValidityType.ValidForDate},
                                                   new Operator {Label = "Tomorrow", Value = "tomorrow", TypeValidity = Operator.ValidityType.ValidForDate},
                                                   new Operator {Label = "Last 7 days", Value = "last-seven-days", TypeValidity = Operator.ValidityType.ValidForDate},
                                                   new Operator {Label = "Next 7 days", Value = "next-seven-days", TypeValidity = Operator.ValidityType.ValidForDate},
                                                   new Operator {Label = "Last week", Value = "last-week", TypeValidity = Operator.ValidityType.ValidForDate},
                                                   new Operator {Label = "This week", Value = "this-week", TypeValidity = Operator.ValidityType.ValidForDate},
                                                   new Operator {Label = "Next week", Value = "next-week", TypeValidity = Operator.ValidityType.ValidForDate},
                                                   new Operator {Label = "Last month", Value = "last-month", TypeValidity = Operator.ValidityType.ValidForDate},
                                                   new Operator {Label = "This month", Value = "this-month", TypeValidity = Operator.ValidityType.ValidForDate},
                                                   new Operator {Label = "Next month", Value = "next-month", TypeValidity = Operator.ValidityType.ValidForDate},
                                                   new Operator {Label = "On", Value = "on", TypeValidity = Operator.ValidityType.ValidForDate},
                                                   new Operator {Label = "On or before", Value = "on-or-before", TypeValidity = Operator.ValidityType.ValidForDate},
                                                   new Operator {Label = "On or after", Value = "on-or-after", TypeValidity = Operator.ValidityType.ValidForDate},
                                                   new Operator {Label = "Last year", Value = "last-year", TypeValidity = Operator.ValidityType.ValidForDate},
                                                   new Operator {Label = "This year", Value = "this-year", TypeValidity = Operator.ValidityType.ValidForDate},
                                                   new Operator {Label = "Next year", Value = "next-year", TypeValidity = Operator.ValidityType.ValidForDate},
                                                   new Operator {Label = "Last X hours", Value = "last-x-hours", TypeValidity = Operator.ValidityType.ValidForDate},
                                                   new Operator {Label = "Next X hours", Value = "next-x-hours", TypeValidity = Operator.ValidityType.ValidForDate},
                                                   new Operator {Label = "Last X days", Value = "last-x-days", TypeValidity = Operator.ValidityType.ValidForDate},
                                                   new Operator {Label = "Next X days", Value = "next-x-days", TypeValidity = Operator.ValidityType.ValidForDate},
                                                   new Operator {Label = "Last X weeks", Value = "last-x-weeks", TypeValidity = Operator.ValidityType.ValidForDate},
                                                   new Operator {Label = "Next X weeks", Value = "next-x-weeks", TypeValidity = Operator.ValidityType.ValidForDate},
                                                   new Operator {Label = "Last X months", Value = "last-x-months", TypeValidity = Operator.ValidityType.ValidForDate},
                                                   new Operator {Label = "Next X months", Value = "next-x-months", TypeValidity = Operator.ValidityType.ValidForDate},
                                                   new Operator {Label = "Older than X months", Value = "olderthan-x-months", TypeValidity = Operator.ValidityType.ValidForDate},
                                                   new Operator {Label = "Last X years", Value = "last-x-years", TypeValidity = Operator.ValidityType.ValidForDate},
                                                   new Operator {Label = "Next X years", Value = "next-x-years", TypeValidity = Operator.ValidityType.ValidForDate},
                                                   new Operator {Label = "Equal current user Id", Value = "eq-userid", TypeValidity = Operator.ValidityType.ValidForUser},
                                                   new Operator {Label = "Not equal current user Id", Value = "ne-userid", TypeValidity = Operator.ValidityType.ValidForUser},
                                                   new Operator {Label = "Equal current user teams", Value = "eq-userteams", TypeValidity = Operator.ValidityType.ValidForTeam},
                                                   new Operator {Label = "Equal current user business unit", Value = "eq-businessid", TypeValidity = Operator.ValidityType.ValidForBusinessUnit},
                                                   new Operator {Label = "Not equals current user business unit", Value = "ne-businessid", TypeValidity = Operator.ValidityType.ValidForBusinessUnit},
                                                   new Operator {Label = "Equal user language", Value = "eq-userlanguage", TypeValidity = Operator.ValidityType.ValidForDate},
                                                   new Operator {Label = "This fiscal year", Value = "this-fiscal-year", TypeValidity = Operator.ValidityType.ValidForDate},
                                                   new Operator {Label = "This fiscal period", Value = "this-fiscal-period", TypeValidity = Operator.ValidityType.ValidForDate},
                                                   new Operator {Label = "Next fiscal year", Value = "next-fiscal-year", TypeValidity = Operator.ValidityType.ValidForDate},
                                                   new Operator {Label = "Next fiscal period", Value = "next-fiscal-period", TypeValidity = Operator.ValidityType.ValidForDate},
                                                   new Operator {Label = "Last fiscal year", Value = "last-fiscal-year", TypeValidity = Operator.ValidityType.ValidForDate},
                                                   new Operator {Label = "Last fiscal period", Value = "last-fiscal-period", TypeValidity = Operator.ValidityType.ValidForDate},
                                                   new Operator {Label = "Last X fiscal years", Value = "last-x-fiscal-years", TypeValidity = Operator.ValidityType.ValidForDate},
                                                   new Operator {Label = "Last X fiscal period", Value = "last-x-fiscal-periods", TypeValidity = Operator.ValidityType.ValidForDate},
                                                   new Operator {Label = "Next X fiscal years", Value = "next-x-fiscal-years", TypeValidity = Operator.ValidityType.ValidForDate},
                                                   new Operator {Label = "Next X fiscal period", Value = "next-x-fiscal-periods", TypeValidity = Operator.ValidityType.ValidForDate},
                                                   new Operator {Label = "In fiscal year", Value = "in-fiscal-year", TypeValidity = Operator.ValidityType.ValidForDate},
                                                   new Operator {Label = "In fiscal period", Value = "in-fiscal-period", TypeValidity = Operator.ValidityType.ValidForDate},
                                                   new Operator {Label = "In fiscal period and year", Value = "in-fiscal-period-and-year", TypeValidity = Operator.ValidityType.ValidForDate},
                                                   new Operator
                                                       {
                                                           Label = "In or before fiscal period and year",
                                                           Value = "in-or-before-fiscal-period-and-year", TypeValidity = Operator.ValidityType.ValidForDate
                                                       },
                                                   new Operator
                                                       {
                                                           Label = "In or after fiscal period and year",
                                                           Value = "in-or-after-fiscal-period-and-year", TypeValidity = Operator.ValidityType.ValidForDate
                                                       },
                                                   new Operator {Label = "Begin with", Value = "begins-with", TypeValidity = Operator.ValidityType.ValidForText},
                                                   new Operator {Label = "Not begin with", Value = "not-begin-with", TypeValidity = Operator.ValidityType.ValidForText},
                                                   new Operator {Label = "End with", Value = "ends-with", TypeValidity = Operator.ValidityType.ValidForText},
                                                   new Operator {Label = "Not end with", Value = "not-end-with", TypeValidity = Operator.ValidityType.ValidForText}
                                               };
    }
}
