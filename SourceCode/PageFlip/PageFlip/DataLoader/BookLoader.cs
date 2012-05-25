using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Collections.Generic;
using PageFlipUltis;
namespace PageFlip.DataLoader
{
	public class Article
	{
		public string ArticleName;
		public int Index;
		public int ParentIndex;
		public UIElement CreateArticlePage(int iIndex, string strXaml)
		{

			if (ParentIndex == 0)
			{
				if (iIndex == 0)
				{
					string xaml = @"<Grid x:Name=""pageRoot"" 
								xmlns='http://schemas.microsoft.com/client/2007'
								xmlns:x='http://schemas.microsoft.com/winfx/2006/xaml'
							Width='850' Height='600' Margin=""10""
							Background=""White"">
					<Grid.RowDefinitions>
						<RowDefinition Height=""0.29*""/>
						<RowDefinition Height=""0.405*""/>
						<RowDefinition Height=""0.305*""/>
					</Grid.RowDefinitions>
					<Grid.ColumnDefinitions>
						<ColumnDefinition Width=""0.249*""/>
						<ColumnDefinition Width=""0.084*""/>
						<ColumnDefinition Width=""0.118*""/>
						<ColumnDefinition Width=""0.217*""/>
						<ColumnDefinition Width=""0.332*""/>
					</Grid.ColumnDefinitions>
					<RichTextBlock Grid.ColumnSpan=""2"" Margin=""0,0,10,0"" TextAlignment=""Justify"" IsTextSelectionEnabled=""False""  x:Name=""rtb_01"" FontSize=""12"" OverflowContentTarget=""{Binding ElementName=rtb_02}"">
						<Paragraph FontSize=""18"" FontWeight=""Bold"">They said/We said: With news of Schiaparelli’s relaunch, John Galliano’s name is being thrown into the ring
						</Paragraph>
						<Paragraph >More than 50 years after its shuttering (and almost 40 years after the death of its brilliant founder), the house of Schiaparelli is set to relaunch just as its name once again reaches the prominence it had in the pre-war years.
						</Paragraph>
						<Paragraph>To coincide with the opening of the Met Costume Institute’s retrospective exhibit Schiaparelli and Prada: Impossible Conversations, Italian business tycoon Diego Della Valle announced the official relaunch earlier this week. Though the brand has remained dormant, even since being acquired by the titan in 2006, Della Valle plans on giving the old house a contemporary update, saying that it “doesn’t have to get involved in the frenetic world of numbers, accounts and dimensions, but it just has to express itself at its best.” 
						</Paragraph>
						<Paragraph>Since Schiaparelli is expected to surpass Tom Ford and Chanel in price point, we think that’s Della Valle’s way of saying, “Don’t worry about how much it costs, you probably can’t afford it anyway.” With no plans for a prêt-à-porter (only prêt-à-couture) collection, no advertising budget, no wholesale plans, no e-commerce sites and only a single location at Place Vendôme, Schiaparelli is slated to become one very exclusive label.
						</Paragraph>
						<Paragraph>The Schiaparelli team has yet to appoint a designer to helm the brand, so naturally the fashion community is abuzz with talk of whose creative vision will steer the newly awakened couture house. W’s Stefano Tonchi threw John Galliano’s name into the mix, saying, “Very soon they will make the announcement, so I already know a little bit too much. But I thought that it could be fun to have John back doing it—as in, Galliano.” To all those Galliano fans out there, too bad Tonchi was speaking in past tense.							 
						</Paragraph>
						<Paragraph FontWeight=""Bold"">
							THEY SAID…
						</Paragraph>
						<Paragraph>Her World Singapore: ""It might have been a good fit, but… Insiders see John Galliano as ‘risky’ and unlikely choice for Schiaparelli."" [Her World Singapore]
						</Paragraph>
						<Paragraph>Diego Della Valle: ""[Just because it] is not commercial, does not mean it won’t be profitable,"" he tells Friedman. ""There are a lot of rich people in the world who want something very special.""[New York Fashion]
						</Paragraph>
						<Paragraph FontWeight=""Bold"">WE SAID…
						</Paragraph>
						<Paragraph>
						Randi Bergman, online editor: “Given Elsa Schiaparelli’s penchant for the absurd and the promise of holier-than-thou price points, it seems like the brand’s relaunch will be all about the show. And if a show’s what they want, Galliano’s surely the man for the job. It seems like a great fit, but I don’t know how excited I will be about my favourite label of all time being helmed by a man that I still haven’t quite forgiven yet.”
						</Paragraph>
					</RichTextBlock>
					<RichTextBlockOverflow Margin=""0,0,10,0"" Grid.Row=""1"" x:Name=""rtb_02"" OverflowContentTarget=""{Binding ElementName=rtb_03}"" ></RichTextBlockOverflow>
					<RichTextBlockOverflow Grid.ColumnSpan=""2"" Margin=""0,0,10,0"" Grid.Row=""2"" x:Name=""rtb_03"" OverflowContentTarget=""{Binding ElementName=rtb_04}""></RichTextBlockOverflow>
					<RichTextBlockOverflow Grid.ColumnSpan=""2"" Grid.Column=""2"" Margin=""0,0,10,0""  x:Name=""rtb_04"" OverflowContentTarget=""{Binding ElementName=rtb_05}""></RichTextBlockOverflow>
					<RichTextBlockOverflow Grid.Column=""3"" Margin=""0,0,10,0"" Grid.Row=""1""  x:Name=""rtb_05"" OverflowContentTarget=""{Binding ElementName=rtb_06}""></RichTextBlockOverflow>
					<RichTextBlockOverflow Grid.ColumnSpan=""2"" Grid.Column=""2"" Margin=""0,0,10,0"" Grid.Row=""2""  x:Name=""rtb_06"" OverflowContentTarget=""{Binding ElementName=rtb_07}""></RichTextBlockOverflow>
					<RichTextBlockOverflow Grid.Column=""4"" Margin=""0,0,0,0"" Grid.RowSpan=""3""  x:Name=""rtb_07""></RichTextBlockOverflow>
					<Border BorderBrush=""Black"" BorderThickness=""1"" Grid.ColumnSpan=""2"" Grid.Column=""1"" Margin=""3"" Grid.Row=""1"">
						<Image Source=""/PageFlip;component/Images/may12-schiap.jpg""></Image>
					</Border>
		
				</Grid>";
					//<RichTextBlockOverflow Grid.ColumnSpan=""2"" Margin=""0,2,2,2""  x:Name=""rtb_07"" ></RichTextBlockOverflow>
					return Ultis.LoadXamlFromString(xaml);
				}
				else
					{
						string xaml = @"<Grid x:Name=""pageRoot"" 
								xmlns='http://schemas.microsoft.com/client/2007'
								xmlns:x='http://schemas.microsoft.com/winfx/2006/xaml'
							Width='850' Height='600' Margin=""10""
							Background=""White"">
						<Grid.RowDefinitions>
							<RowDefinition Height=""0.21*""/>
							<RowDefinition Height=""0.24*""/>
							<RowDefinition Height=""0.245*""/>
							<RowDefinition Height=""0.305*""/>
						</Grid.RowDefinitions>
						<Grid.ColumnDefinitions>
							<ColumnDefinition Width=""0.33*""/>
							<ColumnDefinition Width=""0.33*""/>
							<ColumnDefinition Width=""0.33*""/>
						</Grid.ColumnDefinitions>
						<TextBlock Grid.ColumnSpan=""2"" Margin=""20"" TextWrapping=""Wrap"" Text=""QA: 5 minutes with Alisha Schick"" FontSize=""29.333"" FontWeight=""Bold"" VerticalAlignment=""Center"" HorizontalAlignment=""Center""/>
						<RichTextBlock  Margin=""0,0,10,0"" Grid.Row=""1"" TextAlignment=""Justify"" IsTextSelectionEnabled=""False""  x:Name=""rtb_01"" FontSize=""12"" OverflowContentTarget=""{Binding ElementName=rtb_02}"">
						<Paragraph >This year, one of Edmonton’s local treasures celebrates the 10th anniversary of her ready-to-wear label, Suka Clothing (sukaclothing.ca). Alisha Schick is known for balancing feminine silhouettes with edgy geometric details, and her Spring 2012 show at Western Canada Fashion Week included an array of cool cosmic microprints. Here, the designer and fashion design instructor (she teaches at Edmonton’s MC College) talks secret fashion weapons and childhood fads.
						</Paragraph>
						<Paragraph FontWeight=""Bold"">What is your secret design weapon?</Paragraph>
						<Paragraph>“My sketchbook is my most important tool. I sketch a full figure for key pieces and looks, and then elaborate. There is always a strong story behind each collection.”</Paragraph>
						<Paragraph FontWeight=""Bold"">How has teaching affected your work?</Paragraph>
						<Paragraph>“Being an instructor has challenged my design beliefs and refined my skills and abilities as a designer. I learn as much from my students as they do from me.”</Paragraph>
						<Paragraph FontWeight=""Bold"">In a perfect world, how would we all be dressed?</Paragraph>						
						<Paragraph>“Some of the most beautiful silhouettes and styles [are from the] 1920s and 1950s—any look from those eras would be heaven. It was a time when the focus was on quality design.”</Paragraph>
						<Paragraph FontWeight=""Bold"">When did fashion become a focus for you?</Paragraph>						
						<Paragraph>“In Grade 5, I was the kid who had the asymmetrical haircut and a Beethoven-inspired blouse with a ruffled ascot.”
						</Paragraph>
						<Paragraph FontWeight=""Bold"">What is your secret design weapon?</Paragraph>
						<Paragraph>“My sketchbook is my most important tool. I sketch a full figure for key pieces and looks, and then elaborate. There is always a strong story behind each collection.”</Paragraph>
						<Paragraph FontWeight=""Bold"">How has teaching affected your work?</Paragraph>
						<Paragraph>“Being an instructor has challenged my design beliefs and refined my skills and abilities as a designer. I learn as much from my students as they do from me.”</Paragraph>
						<Paragraph FontWeight=""Bold"">In a perfect world, how would we all be dressed?</Paragraph>						
						<Paragraph>“Some of the most beautiful silhouettes and styles [are from the] 1920s and 1950s—any look from those eras would be heaven. It was a time when the focus was on quality design.”</Paragraph>
						<Paragraph FontWeight=""Bold"">When did fashion become a focus for you?</Paragraph>						
						<Paragraph>“In Grade 5, I was the kid who had the asymmetrical haircut and a Beethoven-inspired blouse with a ruffled ascot.”
						</Paragraph>
						<Paragraph FontWeight=""Bold"">What is your secret design weapon?</Paragraph>
						<Paragraph>“My sketchbook is my most important tool. I sketch a full figure for key pieces and looks, and then elaborate. There is always a strong story behind each collection.”</Paragraph>
						<Paragraph FontWeight=""Bold"">How has teaching affected your work?</Paragraph>
						<Paragraph>“Being an instructor has challenged my design beliefs and refined my skills and abilities as a designer. I learn as much from my students as they do from me.”</Paragraph>
						<Paragraph FontWeight=""Bold"">In a perfect world, how would we all be dressed?</Paragraph>						
						<Paragraph>“Some of the most beautiful silhouettes and styles [are from the] 1920s and 1950s—any look from those eras would be heaven. It was a time when the focus was on quality design.”</Paragraph>
						<Paragraph FontWeight=""Bold"">When did fashion become a focus for you?</Paragraph>						
						<Paragraph>“In Grade 5, I was the kid who had the asymmetrical haircut and a Beethoven-inspired blouse with a ruffled ascot.”
						</Paragraph>
						<Paragraph FontWeight=""Bold"">What is your secret design weapon?</Paragraph>
						<Paragraph>“My sketchbook is my most important tool. I sketch a full figure for key pieces and looks, and then elaborate. There is always a strong story behind each collection.”</Paragraph>
						<Paragraph FontWeight=""Bold"">How has teaching affected your work?</Paragraph>
						<Paragraph>“Being an instructor has challenged my design beliefs and refined my skills and abilities as a designer. I learn as much from my students as they do from me.”</Paragraph>
						<Paragraph FontWeight=""Bold"">In a perfect world, how would we all be dressed?</Paragraph>						
						<Paragraph>“Some of the most beautiful silhouettes and styles [are from the] 1920s and 1950s—any look from those eras would be heaven. It was a time when the focus was on quality design.”</Paragraph>
						<Paragraph FontWeight=""Bold"">When did fashion become a focus for you?</Paragraph>						
						<Paragraph>“In Grade 5, I was the kid who had the asymmetrical haircut and a Beethoven-inspired blouse with a ruffled ascot.”
						</Paragraph>
						</RichTextBlock>
						<RichTextBlockOverflow  Margin=""0,0,10,0"" Grid.Row=""2"" x:Name=""rtb_02"" OverflowContentTarget=""{Binding ElementName=rtb_03}"" ></RichTextBlockOverflow>
						<RichTextBlockOverflow  Margin=""0,0,10,0"" Grid.Row=""3"" x:Name=""rtb_03"" OverflowContentTarget=""{Binding ElementName=rtb_04}""></RichTextBlockOverflow>
						<RichTextBlockOverflow Grid.Column=""1"" Margin=""0,0,10,0"" Grid.Row=""1"" x:Name=""rtb_04"" OverflowContentTarget=""{Binding ElementName=rtb_05}""></RichTextBlockOverflow>
						<RichTextBlockOverflow Grid.Column=""1"" Margin=""0,0,10,0"" Grid.Row=""2""  x:Name=""rtb_05"" OverflowContentTarget=""{Binding ElementName=rtb_06}""></RichTextBlockOverflow>
						<RichTextBlockOverflow Grid.Column=""1"" Margin=""0,0,10,0"" Grid.Row=""3"" x:Name=""rtb_06"" OverflowContentTarget=""{Binding ElementName=rtb_07}""></RichTextBlockOverflow>
						<RichTextBlockOverflow Grid.Column=""2"" Margin=""0,0,0,0"" Grid.RowSpan=""2"" Grid.Row=""2""  x:Name=""rtb_07""></RichTextBlockOverflow>
							<Border BorderBrush=""Black"" BorderThickness=""1"" Margin=""5"" Grid.Column=""2"" Grid.RowSpan=""2"">
							<Image Source=""/PageFlip;component/Images/may12AbSh_Schick.jpg"" Margin=""7""/>
						</Border>
						
				</Grid>";
						//<RichTextBlockOverflow Grid.ColumnSpan=""2"" Margin=""0,2,2,2""  x:Name=""rtb_07"" ></RichTextBlockOverflow>

						return Ultis.LoadXamlFromString(xaml);
					}				
			}
			else
				if (ParentIndex == 1)
				{
					if (iIndex == 0)
					{
						string xaml = @"<Grid x:Name=""pageRoot"" 
								xmlns='http://schemas.microsoft.com/client/2007'
								xmlns:x='http://schemas.microsoft.com/winfx/2006/xaml'
							Width='850' Height='600' Margin=""10""
							Background=""White"">
					<Grid.RowDefinitions>
						<RowDefinition Height=""0.29*""/>
						<RowDefinition Height=""0.405*""/>
						<RowDefinition Height=""0.305*""/>
					</Grid.RowDefinitions>
					<Grid.ColumnDefinitions>
						<ColumnDefinition Width=""0.249*""/>
						<ColumnDefinition Width=""0.084*""/>
						<ColumnDefinition Width=""0.118*""/>
						<ColumnDefinition Width=""0.217*""/>
						<ColumnDefinition Width=""0.332*""/>
					</Grid.ColumnDefinitions>
					<RichTextBlock Grid.ColumnSpan=""2"" Margin=""0,0,10,0"" TextAlignment=""Justify"" IsTextSelectionEnabled=""False""  x:Name=""rtb_01"" FontSize=""12"" OverflowContentTarget=""{Binding ElementName=rtb_02}"">
						<Paragraph FontSize=""18"" FontWeight=""Bold"">They said/We said: With news of Schiaparelli’s relaunch, John Galliano’s name is being thrown into the ring
						</Paragraph>
						<Paragraph >More than 50 years after its shuttering (and almost 40 years after the death of its brilliant founder), the house of Schiaparelli is set to relaunch just as its name once again reaches the prominence it had in the pre-war years.
						</Paragraph>
						<Paragraph>To coincide with the opening of the Met Costume Institute’s retrospective exhibit Schiaparelli and Prada: Impossible Conversations, Italian business tycoon Diego Della Valle announced the official relaunch earlier this week. Though the brand has remained dormant, even since being acquired by the titan in 2006, Della Valle plans on giving the old house a contemporary update, saying that it “doesn’t have to get involved in the frenetic world of numbers, accounts and dimensions, but it just has to express itself at its best.” 
						</Paragraph>
						<Paragraph>Since Schiaparelli is expected to surpass Tom Ford and Chanel in price point, we think that’s Della Valle’s way of saying, “Don’t worry about how much it costs, you probably can’t afford it anyway.” With no plans for a prêt-à-porter (only prêt-à-couture) collection, no advertising budget, no wholesale plans, no e-commerce sites and only a single location at Place Vendôme, Schiaparelli is slated to become one very exclusive label.
						</Paragraph>
						<Paragraph>The Schiaparelli team has yet to appoint a designer to helm the brand, so naturally the fashion community is abuzz with talk of whose creative vision will steer the newly awakened couture house. W’s Stefano Tonchi threw John Galliano’s name into the mix, saying, “Very soon they will make the announcement, so I already know a little bit too much. But I thought that it could be fun to have John back doing it—as in, Galliano.” To all those Galliano fans out there, too bad Tonchi was speaking in past tense.							 
						</Paragraph>
						<Paragraph FontWeight=""Bold"">
							THEY SAID…
						</Paragraph>
						<Paragraph>Her World Singapore: ""It might have been a good fit, but… Insiders see John Galliano as ‘risky’ and unlikely choice for Schiaparelli."" [Her World Singapore]
						</Paragraph>
						<Paragraph>Diego Della Valle: ""[Just because it] is not commercial, does not mean it won’t be profitable,"" he tells Friedman. ""There are a lot of rich people in the world who want something very special.""[New York Fashion]
						</Paragraph>
						<Paragraph FontWeight=""Bold"">WE SAID…
						</Paragraph>
						<Paragraph>
						Randi Bergman, online editor: “Given Elsa Schiaparelli’s penchant for the absurd and the promise of holier-than-thou price points, it seems like the brand’s relaunch will be all about the show. And if a show’s what they want, Galliano’s surely the man for the job. It seems like a great fit, but I don’t know how excited I will be about my favourite label of all time being helmed by a man that I still haven’t quite forgiven yet.”
						</Paragraph>
					</RichTextBlock>
					<RichTextBlockOverflow Margin=""0,0,10,0"" Grid.Row=""1"" x:Name=""rtb_02"" OverflowContentTarget=""{Binding ElementName=rtb_03}"" ></RichTextBlockOverflow>
					<RichTextBlockOverflow Grid.ColumnSpan=""2"" Margin=""0,0,10,0"" Grid.Row=""2"" x:Name=""rtb_03"" OverflowContentTarget=""{Binding ElementName=rtb_04}""></RichTextBlockOverflow>
					<RichTextBlockOverflow Grid.ColumnSpan=""2"" Grid.Column=""2"" Margin=""0,0,10,0""  x:Name=""rtb_04"" OverflowContentTarget=""{Binding ElementName=rtb_05}""></RichTextBlockOverflow>
					<RichTextBlockOverflow Grid.Column=""3"" Margin=""0,0,10,0"" Grid.Row=""1""  x:Name=""rtb_05"" OverflowContentTarget=""{Binding ElementName=rtb_06}""></RichTextBlockOverflow>
					<RichTextBlockOverflow Grid.ColumnSpan=""2"" Grid.Column=""2"" Margin=""0,0,10,0"" Grid.Row=""2""  x:Name=""rtb_06"" OverflowContentTarget=""{Binding ElementName=rtb_07}""></RichTextBlockOverflow>
					<RichTextBlockOverflow Grid.Column=""4"" Margin=""0,0,0,0"" Grid.RowSpan=""3""  x:Name=""rtb_07""></RichTextBlockOverflow>
					<Border BorderBrush=""Black"" BorderThickness=""1"" Grid.ColumnSpan=""2"" Grid.Column=""1"" Margin=""3"" Grid.Row=""1"">
						<Image Source=""/PageFlip;component/Images/may12Downton1.jpg""></Image>
					</Border>
		
				</Grid>";
						//<RichTextBlockOverflow Grid.ColumnSpan=""2"" Margin=""0,2,2,2""  x:Name=""rtb_07"" ></RichTextBlockOverflow>
						return Ultis.LoadXamlFromString(xaml);
					}
					else
					{
						string xaml = @"<Grid x:Name=""pageRoot"" 
								xmlns='http://schemas.microsoft.com/client/2007'
								xmlns:x='http://schemas.microsoft.com/winfx/2006/xaml'
							Width='850' Height='600' Margin=""10""
							Background=""White"">
						<Grid.RowDefinitions>
							<RowDefinition Height=""0.21*""/>
							<RowDefinition Height=""0.24*""/>
							<RowDefinition Height=""0.245*""/>
							<RowDefinition Height=""0.305*""/>
						</Grid.RowDefinitions>
						<Grid.ColumnDefinitions>
							<ColumnDefinition Width=""0.33*""/>
							<ColumnDefinition Width=""0.33*""/>
							<ColumnDefinition Width=""0.33*""/>
						</Grid.ColumnDefinitions>
						<TextBlock Grid.ColumnSpan=""2"" Margin=""20"" TextWrapping=""Wrap"" Text=""QA: 5 minutes with Alisha Schick"" FontSize=""29.333"" FontWeight=""Bold"" VerticalAlignment=""Center"" HorizontalAlignment=""Center""/>
						<RichTextBlock  Margin=""0,0,10,0"" Grid.Row=""1"" TextAlignment=""Justify"" IsTextSelectionEnabled=""False""  x:Name=""rtb_01"" FontSize=""12"" OverflowContentTarget=""{Binding ElementName=rtb_02}"">
						<Paragraph >This year, one of Edmonton’s local treasures celebrates the 10th anniversary of her ready-to-wear label, Suka Clothing (sukaclothing.ca). Alisha Schick is known for balancing feminine silhouettes with edgy geometric details, and her Spring 2012 show at Western Canada Fashion Week included an array of cool cosmic microprints. Here, the designer and fashion design instructor (she teaches at Edmonton’s MC College) talks secret fashion weapons and childhood fads.
						</Paragraph>
						<Paragraph FontWeight=""Bold"">What is your secret design weapon?</Paragraph>
						<Paragraph>“My sketchbook is my most important tool. I sketch a full figure for key pieces and looks, and then elaborate. There is always a strong story behind each collection.”</Paragraph>
						<Paragraph FontWeight=""Bold"">How has teaching affected your work?</Paragraph>
						<Paragraph>“Being an instructor has challenged my design beliefs and refined my skills and abilities as a designer. I learn as much from my students as they do from me.”</Paragraph>
						<Paragraph FontWeight=""Bold"">In a perfect world, how would we all be dressed?</Paragraph>						
						<Paragraph>“Some of the most beautiful silhouettes and styles [are from the] 1920s and 1950s—any look from those eras would be heaven. It was a time when the focus was on quality design.”</Paragraph>
						<Paragraph FontWeight=""Bold"">When did fashion become a focus for you?</Paragraph>						
						<Paragraph>“In Grade 5, I was the kid who had the asymmetrical haircut and a Beethoven-inspired blouse with a ruffled ascot.”
						</Paragraph>
						<Paragraph FontWeight=""Bold"">What is your secret design weapon?</Paragraph>
						<Paragraph>“My sketchbook is my most important tool. I sketch a full figure for key pieces and looks, and then elaborate. There is always a strong story behind each collection.”</Paragraph>
						<Paragraph FontWeight=""Bold"">How has teaching affected your work?</Paragraph>
						<Paragraph>“Being an instructor has challenged my design beliefs and refined my skills and abilities as a designer. I learn as much from my students as they do from me.”</Paragraph>
						<Paragraph FontWeight=""Bold"">In a perfect world, how would we all be dressed?</Paragraph>						
						<Paragraph>“Some of the most beautiful silhouettes and styles [are from the] 1920s and 1950s—any look from those eras would be heaven. It was a time when the focus was on quality design.”</Paragraph>
						<Paragraph FontWeight=""Bold"">When did fashion become a focus for you?</Paragraph>						
						<Paragraph>“In Grade 5, I was the kid who had the asymmetrical haircut and a Beethoven-inspired blouse with a ruffled ascot.”
						</Paragraph>
						<Paragraph FontWeight=""Bold"">What is your secret design weapon?</Paragraph>
						<Paragraph>“My sketchbook is my most important tool. I sketch a full figure for key pieces and looks, and then elaborate. There is always a strong story behind each collection.”</Paragraph>
						<Paragraph FontWeight=""Bold"">How has teaching affected your work?</Paragraph>
						<Paragraph>“Being an instructor has challenged my design beliefs and refined my skills and abilities as a designer. I learn as much from my students as they do from me.”</Paragraph>
						<Paragraph FontWeight=""Bold"">In a perfect world, how would we all be dressed?</Paragraph>						
						<Paragraph>“Some of the most beautiful silhouettes and styles [are from the] 1920s and 1950s—any look from those eras would be heaven. It was a time when the focus was on quality design.”</Paragraph>
						<Paragraph FontWeight=""Bold"">When did fashion become a focus for you?</Paragraph>						
						<Paragraph>“In Grade 5, I was the kid who had the asymmetrical haircut and a Beethoven-inspired blouse with a ruffled ascot.”
						</Paragraph>
						<Paragraph FontWeight=""Bold"">What is your secret design weapon?</Paragraph>
						<Paragraph>“My sketchbook is my most important tool. I sketch a full figure for key pieces and looks, and then elaborate. There is always a strong story behind each collection.”</Paragraph>
						<Paragraph FontWeight=""Bold"">How has teaching affected your work?</Paragraph>
						<Paragraph>“Being an instructor has challenged my design beliefs and refined my skills and abilities as a designer. I learn as much from my students as they do from me.”</Paragraph>
						<Paragraph FontWeight=""Bold"">In a perfect world, how would we all be dressed?</Paragraph>						
						<Paragraph>“Some of the most beautiful silhouettes and styles [are from the] 1920s and 1950s—any look from those eras would be heaven. It was a time when the focus was on quality design.”</Paragraph>
						<Paragraph FontWeight=""Bold"">When did fashion become a focus for you?</Paragraph>						
						<Paragraph>“In Grade 5, I was the kid who had the asymmetrical haircut and a Beethoven-inspired blouse with a ruffled ascot.”
						</Paragraph>
						</RichTextBlock>
						<RichTextBlockOverflow  Margin=""0,0,10,0"" Grid.Row=""2"" x:Name=""rtb_02"" OverflowContentTarget=""{Binding ElementName=rtb_03}"" ></RichTextBlockOverflow>
						<RichTextBlockOverflow  Margin=""0,0,10,0"" Grid.Row=""3"" x:Name=""rtb_03"" OverflowContentTarget=""{Binding ElementName=rtb_04}""></RichTextBlockOverflow>
						<RichTextBlockOverflow Grid.Column=""1"" Margin=""0,0,10,0"" Grid.Row=""1"" x:Name=""rtb_04"" OverflowContentTarget=""{Binding ElementName=rtb_05}""></RichTextBlockOverflow>
						<RichTextBlockOverflow Grid.Column=""1"" Margin=""0,0,10,0"" Grid.Row=""2""  x:Name=""rtb_05"" OverflowContentTarget=""{Binding ElementName=rtb_06}""></RichTextBlockOverflow>
						<RichTextBlockOverflow Grid.Column=""1"" Margin=""0,0,10,0"" Grid.Row=""3"" x:Name=""rtb_06"" OverflowContentTarget=""{Binding ElementName=rtb_07}""></RichTextBlockOverflow>
						<RichTextBlockOverflow Grid.Column=""2"" Margin=""0,0,0,0"" Grid.RowSpan=""2"" Grid.Row=""2""  x:Name=""rtb_07""></RichTextBlockOverflow>
							<Border BorderBrush=""Black"" BorderThickness=""1"" Margin=""5"" Grid.Column=""2"" Grid.RowSpan=""2"">
							<Image Source=""/PageFlip;component/Images/oct11TFW_CALA22.jpg"" Margin=""7""/>
						</Border>
						
				</Grid>";
						//<RichTextBlockOverflow Grid.ColumnSpan=""2"" Margin=""0,2,2,2""  x:Name=""rtb_07"" ></RichTextBlockOverflow>

						return Ultis.LoadXamlFromString(xaml);
					}		
				
				}
				else
				{
					if (iIndex == 0)
					{
						string xaml = @"<Grid x:Name=""pageRoot"" 
								xmlns='http://schemas.microsoft.com/client/2007'
								xmlns:x='http://schemas.microsoft.com/winfx/2006/xaml'
							Width='850' Height='600' Margin=""10""
							Background=""White"">
					<Grid.RowDefinitions>
						<RowDefinition Height=""0.29*""/>
						<RowDefinition Height=""0.405*""/>
						<RowDefinition Height=""0.305*""/>
					</Grid.RowDefinitions>
					<Grid.ColumnDefinitions>
						<ColumnDefinition Width=""0.249*""/>
						<ColumnDefinition Width=""0.084*""/>
						<ColumnDefinition Width=""0.118*""/>
						<ColumnDefinition Width=""0.217*""/>
						<ColumnDefinition Width=""0.332*""/>
					</Grid.ColumnDefinitions>
					<RichTextBlock Grid.ColumnSpan=""2"" Margin=""0,0,10,0"" TextAlignment=""Justify"" IsTextSelectionEnabled=""False""  x:Name=""rtb_01"" FontSize=""12"" OverflowContentTarget=""{Binding ElementName=rtb_02}"">
						<Paragraph FontSize=""18"" FontWeight=""Bold"">They said/We said: With news of Schiaparelli’s relaunch, John Galliano’s name is being thrown into the ring
						</Paragraph>
						<Paragraph >More than 50 years after its shuttering (and almost 40 years after the death of its brilliant founder), the house of Schiaparelli is set to relaunch just as its name once again reaches the prominence it had in the pre-war years.
						</Paragraph>
						<Paragraph>To coincide with the opening of the Met Costume Institute’s retrospective exhibit Schiaparelli and Prada: Impossible Conversations, Italian business tycoon Diego Della Valle announced the official relaunch earlier this week. Though the brand has remained dormant, even since being acquired by the titan in 2006, Della Valle plans on giving the old house a contemporary update, saying that it “doesn’t have to get involved in the frenetic world of numbers, accounts and dimensions, but it just has to express itself at its best.” 
						</Paragraph>
						<Paragraph>Since Schiaparelli is expected to surpass Tom Ford and Chanel in price point, we think that’s Della Valle’s way of saying, “Don’t worry about how much it costs, you probably can’t afford it anyway.” With no plans for a prêt-à-porter (only prêt-à-couture) collection, no advertising budget, no wholesale plans, no e-commerce sites and only a single location at Place Vendôme, Schiaparelli is slated to become one very exclusive label.
						</Paragraph>
						<Paragraph>The Schiaparelli team has yet to appoint a designer to helm the brand, so naturally the fashion community is abuzz with talk of whose creative vision will steer the newly awakened couture house. W’s Stefano Tonchi threw John Galliano’s name into the mix, saying, “Very soon they will make the announcement, so I already know a little bit too much. But I thought that it could be fun to have John back doing it—as in, Galliano.” To all those Galliano fans out there, too bad Tonchi was speaking in past tense.							 
						</Paragraph>
						<Paragraph FontWeight=""Bold"">
							THEY SAID…
						</Paragraph>
						<Paragraph>Her World Singapore: ""It might have been a good fit, but… Insiders see John Galliano as ‘risky’ and unlikely choice for Schiaparelli."" [Her World Singapore]
						</Paragraph>
						<Paragraph>Diego Della Valle: ""[Just because it] is not commercial, does not mean it won’t be profitable,"" he tells Friedman. ""There are a lot of rich people in the world who want something very special.""[New York Fashion]
						</Paragraph>
						<Paragraph FontWeight=""Bold"">WE SAID…
						</Paragraph>
						<Paragraph>
						Randi Bergman, online editor: “Given Elsa Schiaparelli’s penchant for the absurd and the promise of holier-than-thou price points, it seems like the brand’s relaunch will be all about the show. And if a show’s what they want, Galliano’s surely the man for the job. It seems like a great fit, but I don’t know how excited I will be about my favourite label of all time being helmed by a man that I still haven’t quite forgiven yet.”
						</Paragraph>
					</RichTextBlock>
					<RichTextBlockOverflow Margin=""0,0,10,0"" Grid.Row=""1"" x:Name=""rtb_02"" OverflowContentTarget=""{Binding ElementName=rtb_03}"" ></RichTextBlockOverflow>
					<RichTextBlockOverflow Grid.ColumnSpan=""2"" Margin=""0,0,10,0"" Grid.Row=""2"" x:Name=""rtb_03"" OverflowContentTarget=""{Binding ElementName=rtb_04}""></RichTextBlockOverflow>
					<RichTextBlockOverflow Grid.ColumnSpan=""2"" Grid.Column=""2"" Margin=""0,0,10,0""  x:Name=""rtb_04"" OverflowContentTarget=""{Binding ElementName=rtb_05}""></RichTextBlockOverflow>
					<RichTextBlockOverflow Grid.Column=""3"" Margin=""0,0,10,0"" Grid.Row=""1""  x:Name=""rtb_05"" OverflowContentTarget=""{Binding ElementName=rtb_06}""></RichTextBlockOverflow>
					<RichTextBlockOverflow Grid.ColumnSpan=""2"" Grid.Column=""2"" Margin=""0,0,10,0"" Grid.Row=""2""  x:Name=""rtb_06"" OverflowContentTarget=""{Binding ElementName=rtb_07}""></RichTextBlockOverflow>
					<RichTextBlockOverflow Grid.Column=""4"" Margin=""0,0,0,0"" Grid.RowSpan=""3""  x:Name=""rtb_07""></RichTextBlockOverflow>
					<Border BorderBrush=""Black"" BorderThickness=""1"" Grid.ColumnSpan=""2"" Grid.Column=""1"" Margin=""3"" Grid.Row=""1"">
						<Image Source=""/PageFlip;component/Images/MenuThumLvl4.jpg""></Image>
					</Border>
		
				</Grid>";
						//<RichTextBlockOverflow Grid.ColumnSpan=""2"" Margin=""0,2,2,2""  x:Name=""rtb_07"" ></RichTextBlockOverflow>
						return Ultis.LoadXamlFromString(xaml);
					}
					else
					{
						string xaml = @"<Grid x:Name=""pageRoot"" 
								xmlns='http://schemas.microsoft.com/client/2007'
								xmlns:x='http://schemas.microsoft.com/winfx/2006/xaml'
							Width='850' Height='600' Margin=""10""
							Background=""White"">
						<Grid.RowDefinitions>
							<RowDefinition Height=""0.21*""/>
							<RowDefinition Height=""0.24*""/>
							<RowDefinition Height=""0.245*""/>
							<RowDefinition Height=""0.305*""/>
						</Grid.RowDefinitions>
						<Grid.ColumnDefinitions>
							<ColumnDefinition Width=""0.33*""/>
							<ColumnDefinition Width=""0.33*""/>
							<ColumnDefinition Width=""0.33*""/>
						</Grid.ColumnDefinitions>
						<TextBlock Grid.ColumnSpan=""2"" Margin=""20"" TextWrapping=""Wrap"" Text=""QA: 5 minutes with Alisha Schick"" FontSize=""29.333"" FontWeight=""Bold"" VerticalAlignment=""Center"" HorizontalAlignment=""Center""/>
						<RichTextBlock  Margin=""0,0,10,0"" Grid.Row=""1"" TextAlignment=""Justify"" IsTextSelectionEnabled=""False""  x:Name=""rtb_01"" FontSize=""12"" OverflowContentTarget=""{Binding ElementName=rtb_02}"">
						<Paragraph >This year, one of Edmonton’s local treasures celebrates the 10th anniversary of her ready-to-wear label, Suka Clothing (sukaclothing.ca). Alisha Schick is known for balancing feminine silhouettes with edgy geometric details, and her Spring 2012 show at Western Canada Fashion Week included an array of cool cosmic microprints. Here, the designer and fashion design instructor (she teaches at Edmonton’s MC College) talks secret fashion weapons and childhood fads.
						</Paragraph>
						<Paragraph FontWeight=""Bold"">What is your secret design weapon?</Paragraph>
						<Paragraph>“My sketchbook is my most important tool. I sketch a full figure for key pieces and looks, and then elaborate. There is always a strong story behind each collection.”</Paragraph>
						<Paragraph FontWeight=""Bold"">How has teaching affected your work?</Paragraph>
						<Paragraph>“Being an instructor has challenged my design beliefs and refined my skills and abilities as a designer. I learn as much from my students as they do from me.”</Paragraph>
						<Paragraph FontWeight=""Bold"">In a perfect world, how would we all be dressed?</Paragraph>						
						<Paragraph>“Some of the most beautiful silhouettes and styles [are from the] 1920s and 1950s—any look from those eras would be heaven. It was a time when the focus was on quality design.”</Paragraph>
						<Paragraph FontWeight=""Bold"">When did fashion become a focus for you?</Paragraph>						
						<Paragraph>“In Grade 5, I was the kid who had the asymmetrical haircut and a Beethoven-inspired blouse with a ruffled ascot.”
						</Paragraph>
						<Paragraph FontWeight=""Bold"">What is your secret design weapon?</Paragraph>
						<Paragraph>“My sketchbook is my most important tool. I sketch a full figure for key pieces and looks, and then elaborate. There is always a strong story behind each collection.”</Paragraph>
						<Paragraph FontWeight=""Bold"">How has teaching affected your work?</Paragraph>
						<Paragraph>“Being an instructor has challenged my design beliefs and refined my skills and abilities as a designer. I learn as much from my students as they do from me.”</Paragraph>
						<Paragraph FontWeight=""Bold"">In a perfect world, how would we all be dressed?</Paragraph>						
						<Paragraph>“Some of the most beautiful silhouettes and styles [are from the] 1920s and 1950s—any look from those eras would be heaven. It was a time when the focus was on quality design.”</Paragraph>
						<Paragraph FontWeight=""Bold"">When did fashion become a focus for you?</Paragraph>						
						<Paragraph>“In Grade 5, I was the kid who had the asymmetrical haircut and a Beethoven-inspired blouse with a ruffled ascot.”
						</Paragraph>
						<Paragraph FontWeight=""Bold"">What is your secret design weapon?</Paragraph>
						<Paragraph>“My sketchbook is my most important tool. I sketch a full figure for key pieces and looks, and then elaborate. There is always a strong story behind each collection.”</Paragraph>
						<Paragraph FontWeight=""Bold"">How has teaching affected your work?</Paragraph>
						<Paragraph>“Being an instructor has challenged my design beliefs and refined my skills and abilities as a designer. I learn as much from my students as they do from me.”</Paragraph>
						<Paragraph FontWeight=""Bold"">In a perfect world, how would we all be dressed?</Paragraph>						
						<Paragraph>“Some of the most beautiful silhouettes and styles [are from the] 1920s and 1950s—any look from those eras would be heaven. It was a time when the focus was on quality design.”</Paragraph>
						<Paragraph FontWeight=""Bold"">When did fashion become a focus for you?</Paragraph>						
						<Paragraph>“In Grade 5, I was the kid who had the asymmetrical haircut and a Beethoven-inspired blouse with a ruffled ascot.”
						</Paragraph>
						<Paragraph FontWeight=""Bold"">What is your secret design weapon?</Paragraph>
						<Paragraph>“My sketchbook is my most important tool. I sketch a full figure for key pieces and looks, and then elaborate. There is always a strong story behind each collection.”</Paragraph>
						<Paragraph FontWeight=""Bold"">How has teaching affected your work?</Paragraph>
						<Paragraph>“Being an instructor has challenged my design beliefs and refined my skills and abilities as a designer. I learn as much from my students as they do from me.”</Paragraph>
						<Paragraph FontWeight=""Bold"">In a perfect world, how would we all be dressed?</Paragraph>						
						<Paragraph>“Some of the most beautiful silhouettes and styles [are from the] 1920s and 1950s—any look from those eras would be heaven. It was a time when the focus was on quality design.”</Paragraph>
						<Paragraph FontWeight=""Bold"">When did fashion become a focus for you?</Paragraph>						
						<Paragraph>“In Grade 5, I was the kid who had the asymmetrical haircut and a Beethoven-inspired blouse with a ruffled ascot.”
						</Paragraph>
						</RichTextBlock>
						<RichTextBlockOverflow  Margin=""0,0,10,0"" Grid.Row=""2"" x:Name=""rtb_02"" OverflowContentTarget=""{Binding ElementName=rtb_03}"" ></RichTextBlockOverflow>
						<RichTextBlockOverflow  Margin=""0,0,10,0"" Grid.Row=""3"" x:Name=""rtb_03"" OverflowContentTarget=""{Binding ElementName=rtb_04}""></RichTextBlockOverflow>
						<RichTextBlockOverflow Grid.Column=""1"" Margin=""0,0,10,0"" Grid.Row=""1"" x:Name=""rtb_04"" OverflowContentTarget=""{Binding ElementName=rtb_05}""></RichTextBlockOverflow>
						<RichTextBlockOverflow Grid.Column=""1"" Margin=""0,0,10,0"" Grid.Row=""2""  x:Name=""rtb_05"" OverflowContentTarget=""{Binding ElementName=rtb_06}""></RichTextBlockOverflow>
						<RichTextBlockOverflow Grid.Column=""1"" Margin=""0,0,10,0"" Grid.Row=""3"" x:Name=""rtb_06"" OverflowContentTarget=""{Binding ElementName=rtb_07}""></RichTextBlockOverflow>
						<RichTextBlockOverflow Grid.Column=""2"" Margin=""0,0,0,0"" Grid.RowSpan=""2"" Grid.Row=""2""  x:Name=""rtb_07""></RichTextBlockOverflow>
							<Border BorderBrush=""Black"" BorderThickness=""1"" Margin=""5"" Grid.Column=""2"" Grid.RowSpan=""2"">
							<Image Source=""/PageFlip;component/Images/MenuThumLvl2.jpg"" Margin=""7""/>
						</Border>
						
				</Grid>";
						//<RichTextBlockOverflow Grid.ColumnSpan=""2"" Margin=""0,2,2,2""  x:Name=""rtb_07"" ></RichTextBlockOverflow>

						return Ultis.LoadXamlFromString(xaml);
					}
				
				}

			
		}

		public void AddParent(TilePageMenu chr)
		{
			//this.ParentIndex = chr.Index;
		}
	}

	public class BookLoader
	{
		List<TilePage> listMenuPage;

		public UIElement CurrentPage;
		public UIElement NextPageLeftPart;
		public UIElement NextPageRightPart;

		public BookLoader()
		{
			listMenuPage = new List<TilePage>();
			//load all chapter infomation
			for (int i = 0; i < 3; i++)
			{
				TilePage MnPg = TilePageMenu.Load(i);
				listMenuPage.Add(MnPg);
			}
		}

        #region MainMenuPage
        int iCurrentMenuIndex = 0;
        int iCurrentMenuLevel = 0;

        public void LoadMainMenu(int idx)
        {
            iCurrentMenuIndex = idx;
            iCurrentMenuLevel = 0;

            if (idx - 1 >= 0)
            {
                //previous page = listMenuPage[idx - 1].CreateMenuPageFromTiles();
            }

            CurrentPage = listMenuPage[idx].generatePage();

            if (idx + 1 < listMenuPage.Count)
            {
                NextPageRightPart = listMenuPage[idx + 1].generatePage();
                NextPageLeftPart = listMenuPage[idx + 1].generatePage();
            }
            else
            {
                NextPageRightPart = null;
                NextPageLeftPart = null;
            }
        }

        public void UpdateAfter_NextMainMenuPage()
        {
            //previous page = CurrentPage;
            CurrentPage = NextPageLeftPart;

            iCurrentMenuIndex++;
            if (iCurrentMenuIndex + 1 < listMenuPage.Count)
            {
                NextPageLeftPart = listMenuPage[iCurrentMenuIndex + 1].generatePage();
                NextPageRightPart = listMenuPage[iCurrentMenuIndex + 1].generatePage();
            }
            else
            {
                NextPageRightPart = null;
                NextPageLeftPart = null;
            }
        }

        public void UpdateAfter_PreviousMainMenuPage()
        {

        }
        #endregion
        public void NextPage()
        {
            UpdateAfter_NextMainMenuPage();
        }

        public bool IsCanTransitionRight()
        {
            if (NextPageRightPart == null && NextPageLeftPart == null) 
                return false;
            return true;
        }
    }
}
