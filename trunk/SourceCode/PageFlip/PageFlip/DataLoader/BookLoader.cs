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
        public int ParentIndex;
        public UIElement CreateArticlePage(int iIndex)
        {
            Button bt = new Button();
            bt.Width = 200;
            bt.Height = 25;
            bt.Content = "Chapter " + this.ParentIndex.ToString() + " Article " + this.Index.ToString() + " Page " + iIndex.ToString();
            return bt;
        }

        public void AddParent(Chapter chr)
        {
            this.ParentIndex = chr.Index;
        }
    }

    public class Chapter
    {
        public List<Article> Articles;
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
            chp.Index = iNextChapterIndex;

            List<Article> list = new List<Article>();
            for (int i = 0; i < 5; i++)
            {
                Article ar = new Article();
                ar.ArticleName = "ArticleName" + i.ToString();
                ar.Index = i;
                ar.AddParent(chp);

                list.Add(ar);
            }

            chp.Articles = list;
            
            return chp;
        }
    }

    public class BookLoader
    {
        List<Chapter> listChapter;
        //int CurrentChapterIndex;
        //int CurrentArticleIndex;
        //int CurrentArticlePageIndex;

        public UIElement CurrentPage;
        public UIElement NextPageLeftPart;
        public UIElement NextPageRightPart;

        public BookLoader()
        {
            listChapter = new List<Chapter>();
                //load all chapter infomation
                for (int i = 0; i < 3; i++)
                {
                    Chapter nextChap = Chapter.LoadChapter(i);
                    listChapter.Add(nextChap);
                }
        }

        public void UpdateBeforeChangeChapter(int iNextChapterIndex)
        {
            NextPageLeftPart = listChapter[iNextChapterIndex].CreateChapterFromArticleName();
            NextPageRightPart = listChapter[iNextChapterIndex].CreateChapterFromArticleName();
        }

        public void UpdateAfterChangeChapter(int iChapterIndex)
        {
            //load 1 current page ad 2 more next page
            CurrentPage = listChapter[iChapterIndex].CreateChapterFromArticleName();
            NextPageLeftPart = listChapter[iChapterIndex].Articles[0].CreateArticlePage(0);
            NextPageRightPart = listChapter[iChapterIndex].Articles[0].CreateArticlePage(0);
        }
        
//        public void toArticle(int iChapterIndex, int iArticleIndex);//animation RToL//return article first page

////change 1 article page
//        public void nextArticlePage(int iArticleIndex, int iCurrentPageIndex);// can return a article page or next chapter page
//        public void previousArticlePage(int iArticleIndex, int iCurrentPageIndex);// can return a article page or current chapter page
    }
}
