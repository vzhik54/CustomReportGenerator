using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using Palmmedia.ReportGenerator.Core.Parser.Analysis;
using Palmmedia.ReportGenerator.Core.Reporting;

namespace MyCompany.CustomReportTypes
{
    /// <summary>
    /// Creates a text file containing the coverage ratio
    /// </summary>
    public class CoverageRatioReportBuilder : IReportBuilder
    {
        /// <summary>
        /// Gets the report type.
        /// </summary>
        /// <value>
        /// The report type.
        /// </value>
        public string ReportType => "CoverageRatio";

        /// <summary>
        /// Gets or sets the report configuration.
        /// </summary>
        /// <value>
        /// The report configuration.
        /// </value>
        public IReportContext ReportContext { get; set; }

        /// <summary>
        /// Creates a class report.
        /// </summary>
        /// <param name="class">The class.</param>
        /// <param name="fileAnalyses">The file analyses that correspond to the class.</param>
        public void CreateClassReport(Class @class, IEnumerable<FileAnalysis> fileAnalyses)
        {
        }

        /// <summary>
        /// Creates the summary report.
        /// </summary>
        /// <param name="summaryResult">The summary result.</param>
        public void CreateSummaryReport(SummaryResult summaryResult)
        {
            if (summaryResult == null)
            {
                throw new ArgumentNullException(nameof(summaryResult));
            }

            string targetPath = Path.Combine(this.ReportContext.ReportConfiguration.TargetDirectory, "CoverageRatio.txt");
            File.WriteAllText(targetPath, summaryResult.CoverageQuota.GetValueOrDefault().ToString("f0", CultureInfo.InvariantCulture));
        }
    }
}