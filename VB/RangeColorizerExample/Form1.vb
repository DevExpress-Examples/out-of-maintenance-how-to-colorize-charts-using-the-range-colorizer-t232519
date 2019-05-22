Imports System
Imports System.Collections.Generic
Imports System.Drawing
Imports System.Windows.Forms
Imports System.Xml.Linq
Imports DevExpress.XtraCharts

Namespace RangeColorizerExample
	Partial Public Class Form1
		Inherits Form

		Private Const filepath As String = "..//..//Data//HPI.xml"

		Public Sub New()
			InitializeComponent()
		End Sub

		Private Sub Form1_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
'			#Region "#BarSeries"
			' Create and customize a bar series.
			Dim barSeries As New Series() With {
				.DataSource = LoadData(filepath),
				.ArgumentDataMember = "Country",
				.ColorDataMember = "Hpi",
				.View = New SideBySideBarSeriesView()
			}
			barSeries.View.Colorizer = CreateColorizer()
			barSeries.ValueDataMembers.AddRange(New String() { "Product" })
'			#End Region ' #BarSeries

			' Add the series to the ChartControl's Series collection.
			chartControl1.Series.Add(barSeries)

			' Show a title for the values axis.
			CType(chartControl1.Diagram, XYDiagram).AxisY.Title.Text = "GDP per capita, $"
			CType(chartControl1.Diagram, XYDiagram).AxisY.Title.Visibility = DevExpress.Utils.DefaultBoolean.True
		End Sub

		#Region "#RangeColorizer"
		' Creates a range colorizer.
		Private Function CreateColorizer() As ChartColorizerBase
			Dim palette As New Palette("Custom")
			palette.Add(Color.FromArgb(255, 255, 90, 25), Color.FromArgb(255, 255, 90, 25))
			palette.Add(Color.FromArgb(255, 229, 227, 53), Color.FromArgb(255, 229, 227, 53))
			palette.Add(Color.FromArgb(255, 110, 201, 92), Color.FromArgb(255, 110, 201, 92))

			Dim colorizer As New RangeColorizer() With {
				.LegendItemPattern = "{V1} - {V2} HPI",
				.Palette = palette
			}
			colorizer.RangeStops.AddRange(New Double() { 22, 30, 38, 46, 54, 64 })
			Return colorizer
		End Function
		#End Region ' #RangeColorizer

		#Region "#DataLoad"
		Private Class HpiPoint
			Public Property Country() As String
			Public Property Product() As Double
			Public Property Hpi() As Double
		End Class

		' Loads data from an XML data source.
		Private Shared Function LoadData(ByVal filepath As String) As List(Of HpiPoint)
			Dim doc As XDocument = XDocument.Load(filepath)
			Dim points As New List(Of HpiPoint)()
			For Each element As XElement In doc.Element("G20HPIs").Elements("CountryStatistics")
				points.Add(New HpiPoint() With {
					.Country = element.Element("Country").Value,
					.Product = Convert.ToDouble(element.Element("Product").Value),
					.Hpi = Convert.ToDouble(element.Element("HPI").Value)
				})
			Next element
			Return points
		End Function
		#End Region ' #DataLoad
	End Class
End Namespace
