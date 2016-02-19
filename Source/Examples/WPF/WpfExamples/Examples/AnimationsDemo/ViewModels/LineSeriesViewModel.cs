﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LineSeriesViewModel.cs" company="OxyPlot">
//   Copyright (c) 2014 OxyPlot contributors
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace AnimationsDemo
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using OxyPlot;
    using OxyPlot.Annotations;
    using OxyPlot.Axes;
    using OxyPlot.Series;

    public class LineSeriesViewModel : AnimationViewModelBase
    {
        public LineSeriesViewModel()
        {
            var pnls = new List<Pnl>();

            var random = new Random(31);
            var dateTime = DateTime.Today.Add(TimeSpan.FromHours(9));
            for (var pointIndex = 0; pointIndex < 50; pointIndex++)
            {
                pnls.Add(new Pnl
                {
                    Time = dateTime,
                    Value = -200 + random.Next(1000),
                });
                dateTime = dateTime.AddMinutes(1);
            }

            var minimum = pnls.Min(x => x.Value);
            var maximum = pnls.Max(x => x.Value);

            var plotModel = this.PlotModel;
            plotModel.Title = "Line Series Animation Demo";

            var series = new LineSeries
            {
                Title = "P & L",
                ItemsSource = pnls,
                DataFieldX = "Time",
                DataFieldY = "Value",
                Color = OxyColor.Parse("#4CAF50"),
                MarkerSize = 8,
                MarkerFill = OxyColor.Parse("#FFFFFFFF"),
                MarkerStroke = OxyColor.Parse("#4CAF50"),
                MarkerStrokeThickness = 4,
                StrokeThickness = 1,
            };
            plotModel.Series.Add(series);

            var annotation = new LineAnnotation
            {
                Type = LineAnnotationType.Horizontal,
                Y = 0
            };
            plotModel.Annotations.Add(annotation);

            var dateTimeAxis = new DateTimeAxis
            {
                Position = AxisPosition.Bottom,
                IntervalType = DateTimeIntervalType.Hours,
                IntervalLength = 50
            };
            plotModel.Axes.Add(dateTimeAxis);

            var margin = (maximum - minimum) * 0.05;

            var valueAxis = new LinearAxis
            {
                Position = AxisPosition.Left,
                Minimum = minimum - margin,
                Maximum = maximum + margin,
            };
            plotModel.Axes.Add(valueAxis);
        }

        public override bool SupportsEasingFunction { get { return false; } }

        public override void Animate(IEasingFunction easingFunction, TimeSpan duration)
        {
            var plotModel = this.PlotModel;
            var series = plotModel.Series.First() as LineSeries;
            if (series != null)
            {
                plotModel.AnimateSeries(series, easingFunction, duration: duration);
            }
        }
    }
}