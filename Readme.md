<!-- default file list -->
*Files to look at*:

* [Form1.cs](./CS/RangeColorizerExample/Form1.cs) (VB: [Form1.vb](./VB/RangeColorizerExample/Form1.vb))
* [Program.cs](./CS/RangeColorizerExample/Program.cs) (VB: [Program.vb](./VB/RangeColorizerExample/Program.vb))
<!-- default file list end -->
# How to colorize charts using the Range Colorizer 


This example demonstrates how to color each series point in a specific color depending on the Happy Planet Index (HPI) data obtained from a datasource.<br />


<h3>Description</h3>

To accomplish this task, create a range colorizer and assign it to the <strong>SeriesViewBase.Colorizer</strong> property.<br /><br />To access HPI information from the HPI.xml datasource, set the <strong>SeriesBase.ColorDataMember</strong> property to the "HPI" data field.<br />Then, add range stops (data splits in ranges) for the colorizer to the<strong> DoubleCollection</strong> object. This object is accessed using the<strong> RangeColorizer.RangeStops</strong> ptoperty.
<p>&nbsp;</p>
<p>After that, specify the desired palette entries in the <strong>PaletteEntry</strong> objects using their <strong>Color</strong> and <strong>Color2</strong> properties and add <strong>PaletteEntry</strong> objects to the chart palette repository using the <strong>ChartControl.PaletteRepository</strong> property. Then, it is necessary to choose a palette for painting series points via the <strong>RangeColorizer.PaletteName</strong> property.</p>
<p>The colorizer automatically associates each color with the specified data range to colorize series.</p>
<p>To see information on what each color means in the legend, specify {V1} and {V2} patterns using the <strong>RangeColorizer.LegendItemPattern</strong> property.</p>

<br/>


