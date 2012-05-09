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

namespace PageFlip.DataLoader
{
    public class Article
    {
        public string ArticleName;
        public int Index;
    }
    public class Chapter
    {
        List<Article> listArticle;
        public int Index;

        public UIElement CreateChapterFromArticleName()
        {
            Button bt = new Button();
            bt.Width = 100;
            bt.Height = 25;
            bt.Content = "Chapter " + this.Index.ToString();
            return bt;
        }

        public static Chapter LoadChapter(int iNextChapterIndex)
        {
            Chapter chp = new Chapter();
            List<Article> list = new List<Article>();
            for (int i = 0; i < 5; i++)
            {
                Article ar = new Article();
                ar.ArticleName = "ArticleName" + i.ToString();
                ar.Index = i;

                list.Add(ar);
            }

            chp.listArticle = list;
            chp.Index = iNextChapterIndex;
            return chp;
        }
    }
    public class BookLoader
    {
        List<Chapter> listChapter;

        public BookLoader()
        {
            listChapter = new List<Chapter>();
        }

        public UIElement ChangeChapter (int iCurrentChapterIndex, int iNextChapterIndex)
        {
            Chapter nextChap = Chapter.LoadChapter(iNextChapterIndex);
            return nextChap.CreateChapterFromArticleName();
        }

        
        public void toArticle(int iChapterIndex, int iArticleIndex);//animation RToL//return article first page

//change 1 article page
        public void nextArticlePage(int iArticleIndex, int iCurrentPageIndex);// can return a article page or next chapter page
        public void previousArticlePage(int iArticleIndex, int iCurrentPageIndex);// can return a article page or current chapter page
    }
}
