using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2
{
#region Содержимое_отчета
    interface GraphicalComponent
    {
        void Show();
    }
    class Text:GraphicalComponent
    {
        string txt;
        public void Show()
        {
            Console.WriteLine("Текст: " + txt);
        }
        public Text(string s)
        {
            txt = s;
        }
    }
    
    class Diagram : GraphicalComponent
    {
        string nameOfDiagram;
        public void Show()
        {
            Console.WriteLine("Диаграмма с названием: " + nameOfDiagram);
        }
        public Diagram(string s)
        {
            nameOfDiagram = s;
        }
    }

    class Graphic : GraphicalComponent
    {
        string nameOfGraphic;
        public void Show()
        {
            Console.WriteLine("График, изображающий " + nameOfGraphic);
        }
        public Graphic(string s)
        {
            nameOfGraphic = s;
        }
    }
#endregion
    //Product class
    class Report
    {
        List<GraphicalComponent> components = new List<GraphicalComponent>();

        public void Add(GraphicalComponent comp)
        {
            components.Add(comp);
        }

        public void Show()
        {
            foreach( GraphicalComponent gc in components)
            {
                gc.Show();
            }
        }
    }
    //Builder
    abstract class ReportBuilder
    {

        public abstract void AddIntroduction();
        public abstract void AddContent();
        public abstract void AddConclusion();
        public abstract Report GetReport();
    }

    class OrdinaryReportBuilder : ReportBuilder
    {
        Report report = new Report();
        public override void AddConclusion()
        {
            report.Add(new Text("To sum up, this issue has both pros and cons and people will always try to stick to their opinion..."));
        }

        public override void AddContent()
        {
            report.Add(new Diagram("Current state of the problem"));
            report.Add(new Text("Arguments FOR"));
            report.Add(new Text("Arguments AGAINST"));
            report.Add(new Graphic("Weights of args"));
        }

        public override void AddIntroduction()
        {
            report.Add(new Text("Some people believe that..."));
            report.Add(new Diagram("Views of people"));
        }

        public override Report GetReport()
        {
            return report;
        }
    }

    class CoursePaperBuilder : ReportBuilder
    {
        Report report = new Report();
        public override void AddConclusion()
        {
            report.Add(new Text("В ходе работы была разработана Информационная Система, позволяющая автоматизировать основные бизнес-процессы, протекающие в..."));
            report.Add(new Text("Для достижения поставленной цели были рассмотрены процессы, нуждавшиеся в автоматизации, аналоги..."));
            report.Add(new Text("Разработанная система полностью удовлетворяет всем поставленным функциональным требованиям, список которых обозначен в приложении..."));
            report.Add(new Text("К созданной информационной системе была разработана документация, включающая в себя техническое задание, " +
                "программу и методику испытаний, руководство программиста и руководство пользователя..."));
            report.Add(new Text("Для разработанной системы было выполнено компонентное, интеграционное и системное тестирование согласно составленной программе и методике испытаний"));
            report.Add(new Text("Конечно же, данную программу можно улучшить, например, ..."));
        }

        public override void AddContent()
        {
            report.Add(new Text("В данной главе будет описан процесс проектирования самой информационной системы с использованием диаграмм классов и сотрудничества..."));
            report.Add(new Text("Описание процесса"));
            report.Add(new Diagram("Диаграмма классов"));
            report.Add(new Diagram("Диаграмма последовательностей"));
            report.Add(new Diagram("Диаграмма сотрудничества"));
            report.Add(new Text("Описание БД"));
            report.Add(new Diagram("ERD-диаграмма"));
            report.Add(new Text("Описание, почему моя спроектированная система самая лучшая"));
        }

        public override void AddIntroduction()
        {
            report.Add(new Text("На сегодняшний день цифровизация является одной из основных задач нашего государства..."));
            report.Add(new Graphic("Уровень цифровизации в различных сферах"));
            report.Add(new Text("Цель данной работы..."));
            report.Add(new Text("Для достижения поставленной цели должны быть решены следующие задачи..."));
            report.Add(new Text("Методы исследования..."));
            report.Add(new Diagram("Заинтересованность общества в решении проблемы"));
        }

        public override Report GetReport()
        {
            return report;
        }
    }

    //Director
    class Writer
    {
        public ReportBuilder reportbuilder;
        public Writer(ReportBuilder repb)
        {
            reportbuilder = repb;
        }
        //Construct
        public void WriteReport()
        {
            reportbuilder.AddIntroduction();
            reportbuilder.AddContent();
            reportbuilder.AddConclusion();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            ReportBuilder rb = new OrdinaryReportBuilder();
            Writer wr = new Writer(rb);
            wr.WriteReport();
            Report currentReport = rb.GetReport();
            currentReport.Show();

            Console.ReadLine();

            ReportBuilder cpb = new CoursePaperBuilder();
            wr = new Writer(cpb);
            wr.WriteReport();
            currentReport = cpb.GetReport();
            currentReport.Show();

            Console.ReadLine();
        }
    }
}
