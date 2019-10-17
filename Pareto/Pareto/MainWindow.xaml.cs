using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Pareto
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>

    

    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            collection = new List<List<TextBox>>();
            col_of_resh = new List<List<TextBox>>();
            gran = new List<List<TextBox>>();
            subopt = new List<List<TextBox>>();
            lexan = new List<List<TextBox>>();


            Create();

            
            
           
            
        }

        private int count = 200 * 5;
        private List<List<TextBox>> collection;
        private List<List<TextBox>> col_of_resh;
        private List<List<TextBox>> gran;
        private List<List<TextBox>> subopt;
        private List<List<TextBox>> lexan;

        private List<TextBox> Compare()
        {
            List<TextBox> col = new List<TextBox>();
            Label a = new Label();
            foreach(var t in st.Children)
            {
                if(t.GetType() != a.GetType())
                col.Add(t as TextBox);
            }
            MessageBox.Show((col[0] as TextBox).Text);

            

            return col;

        }

      

       

        private void Create()
        {
            int j = 1;
            var rand = new Random();
            for (int i = 0; i < count; i++)
            {

                

                if (i%5==0)
                {
                    var a = new Label();
                    a.Width = 30;
                    a.Content = new TextBlock().Text = j.ToString();
                    a.FontSize = 10;
                    st.Children.Add(a);
                    j++;
                }
               

                var temp = new TextBox();
                temp.Width = 110;
                temp.Height = 40;
                temp.Margin = new Thickness(10, 10, 0, 0);
                st.Children.Add(temp);
                
                switch (i%5)
                {
                    case 0:
                        temp.Text = rand.Next(7000, 60000).ToString();
                        collection.Add(new List<TextBox>()); break;   //1
                    case 1: temp.Text = rand.Next(192, 256).ToString(); break;    //2
                    case 2: temp.Text = rand.Next(800, 2000).ToString(); break;    //3
                    case 3: temp.Text = rand.Next(150, 300).ToString(); break;    //4
                    case 4: temp.Text = rand.Next(2, 16).ToString(); break;    //5
                }
                TextBox t = new TextBox();
                t.Width = temp.Width;
                t.Height = temp.Height;
                t.Margin = temp.Margin;
                t.Text = temp.Text;
                collection[j-2].Add(t);

            }


        }

        private void Execute()
        {
            col_of_resh.Clear();
            List<int> need_delete = new List<int>();
            String history = "";
            int history_counter = 1;

            {
                int j = 0;
                foreach (List<TextBox> a in collection)
                {
                    col_of_resh.Add(new List<TextBox>());
                    foreach (TextBox t in a)
                    {
                        TextBox temp = new TextBox();
                        temp.Width = t.Width;
                        temp.Height = t.Height;
                        temp.Margin = t.Margin;
                        temp.Text = t.Text;
                        col_of_resh[j].Add(temp);
                    }
                    j++;
                }
            }



            for (int i=0; i<collection.Count-1; i++)
            {
                
                for (int k = i + 1; k < collection.Count; k++)
                {
                    bool[] check = new bool[5];

                    for (int j = 0; j < 5; j++)
                    {
                        try
                        {
                            switch ((Tendency.Children[j] as TextBox).Text)
                            {
                                case "+":
                                    if (Convert.ToInt32(collection[i][j].Text) >= Convert.ToInt32(collection[k][j].Text))
                                    {
                                        check[j] = true;
                                    }
                                    else check[j] = false;
                                    break;
                                case "-":
                                    if (Convert.ToInt32(collection[i][j].Text) <= Convert.ToInt32(collection[k][j].Text))
                                    {
                                        check[j] = true;
                                    }
                                    else check[j] = false;
                                    break;
                                default:
                                    col_of_resh.Clear();
                                    throw new Exception("Неправильно выбрано стремление(");

                            }
                        }
                        catch (Exception e)
                        {
                            MessageBox.Show("Ошибка, вы ввели что-то не то(\n" + e.ToString());
                            col_of_resh.Clear();
                            return;
                            
                        }
                    }

                    history_counter++;
                    if (check[0] == check[1] && check[1] == check[2] && check[2] == check[3] && check[3] == check[4])
                    {
                        if (check[0] == true)
                        {
                            need_delete.Add(k);

                            history += (i+1)+"D("+(k+1)+")  ";
                            if(history_counter % (count / 5) == 0)
                                history += "\n";

                        }
                        else
                        {
                            need_delete.Add(i);
                            history += (k+1) + "D(" + (i+1) + ")  ";
                            if (history_counter % (count / 5) == 0)
                                history += "\n";
                        }
                    }
                    else
                    {
                        
                        if (history_counter % (count / 5) == 0)
                            history += "\n";
                    }
                    
                }
                
            }

            need_delete.Sort();
            List<int> new_delete = need_delete.Distinct().ToList<int>();


            for(int i= new_delete.Count-1; i>=0; i--)
            {
                col_of_resh.RemoveAt(need_delete[i]);
            }

            (history_lb.Content as TextBlock).Text = history;

        }

        private void apply()
        {
            int j = 0;
            int ct = 0;
            collection.Clear();
            collection.Add(new List<TextBox>());
            for (int i=0; i<st.Children.Count; i++)
            {
                if(st.Children[i].GetType() == typeof(TextBox))
                {
                    if (j % 5 == 0 && j!=0)
                    {
                        collection.Add(new List<TextBox>());
                        ct++;
                    }
                    TextBox temp = new TextBox();
                    temp.Width = (st.Children[i] as TextBox).Width;
                    temp.Height = (st.Children[i] as TextBox).Height;
                    temp.Margin = (st.Children[i] as TextBox).Margin;
                    temp.Text = (st.Children[i] as TextBox).Text;
                    collection[ct].Add(temp);

                    j++;
                }
                
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Par_Opt.Children.Clear();
            Execute();
            

        }

        private void appl_gr()
        {
            int p1 = -1;
            int p2 = -1;
            int b1 = -1;
            int b2 = -1;
            int f1 = -1;
            int f2 = -1;
            int l1 = -1;
            int l2 = -1;
            int v1 = -1;
            int v2 = -1;

            try
            {
                int j = 0;
                foreach (List<TextBox> a in col_of_resh)
                {
                    gran.Add(new List<TextBox>());
                    foreach (TextBox t in a)
                    {
                        TextBox temp = new TextBox();
                        temp.Width = t.Width;
                        temp.Height = t.Height;
                        temp.Margin = t.Margin;
                        temp.Text = t.Text;
                        gran[j].Add(temp);
                    }
                    j++;
                }

            }
            catch (Exception e)
            {
                MessageBox.Show("ошибка копирования" + e.ToString());
            }
            
            try
            {
                if(!String.IsNullOrEmpty(price.Text))
                    p1 = Convert.ToInt32(price.Text);
                if (!String.IsNullOrEmpty(price2.Text))
                    p2 = Convert.ToInt32(price2.Text);
                if (!String.IsNullOrEmpty(bus.Text ))
                    b1 = Convert.ToInt32(bus.Text);
                if (!String.IsNullOrEmpty(bus2.Text))
                    b2 = Convert.ToInt32(bus2.Text);
                if (!String.IsNullOrEmpty(frequancy.Text))
                    f1 = Convert.ToInt32(frequancy.Text);
                if (!String.IsNullOrEmpty(frequancy2.Text))
                    f2 = Convert.ToInt32(frequancy2.Text);
                if (!String.IsNullOrEmpty(lenght.Text))
                    l1 = Convert.ToInt32(lenght.Text);
                if (!String.IsNullOrEmpty(lenght2.Text))
                    l2 = Convert.ToInt32(lenght2.Text);
                if (!String.IsNullOrEmpty(volume.Text))
                    v1 = Convert.ToInt32(volume.Text);
                if (!String.IsNullOrEmpty(volume2.Text))
                    v2 = Convert.ToInt32(volume2.Text);
            }
            catch(Exception ex)
            {
                MessageBox.Show("Неправильно введены границы\n\n"+ex.ToString());
                return;
            }

            try
            {
                List<int> deleted_string = new List<int>();
                for (int i = 0; i < col_of_resh.Count; i++)
                {
                    if (p1 != -1)
                    {
                        if (Convert.ToInt32(gran[i][0].Text) > p1)
                        {
                            deleted_string.Add(i);
                        }
                    }
                    if (p2 != -1)
                    {
                        if (Convert.ToInt32(gran[i][0].Text) < p2)
                        {
                            deleted_string.Add(i);
                        }
                    }
                    if (b1 != -1)
                    {
                        if (Convert.ToInt32(gran[i][1].Text) > b1)
                        {
                            deleted_string.Add(i);
                        }
                    }
                    if (b2 != -1)
                    {
                        if (Convert.ToInt32(gran[i][1].Text) < b2)
                        {
                            deleted_string.Add(i);
                        }
                    }
                    if (f1 != -1)
                    {
                        if (Convert.ToInt32(gran[i][2].Text) > f1)
                        {
                            deleted_string.Add(i);
                        }
                    }
                    if (f2 != -1)
                    {
                        if (Convert.ToInt32(gran[i][2].Text) < f2)
                        {
                            deleted_string.Add(i);
                        }
                    }
                    if (l1 != -1)
                    {
                        if (Convert.ToInt32(gran[i][3].Text) > l1)
                        {
                            deleted_string.Add(i);
                        }
                    }
                    if (l2 != -1)
                    {
                        if (Convert.ToInt32(gran[i][3].Text) < l2)
                        {
                            deleted_string.Add(i);
                        }
                    }
                    if (v1 != -1)
                    {
                        if (Convert.ToInt32(gran[i][4].Text) > v1)
                        {
                            deleted_string.Add(i);
                        }
                    }
                    if (v2 != -1)
                    {
                        if (Convert.ToInt32(gran[i][4].Text) < v2)
                        {
                            deleted_string.Add(i);
                        }
                    }
                }
                var a = deleted_string.Distinct().ToList();

                for(int i=a.Count-1; i>=0; i--)
                {
                    gran.RemoveAt(a[i]);
                }
            }catch(Exception e)
            {
                MessageBox.Show("Ошибка обработки границ");
            }
            
            int c = 1;
            foreach (List<TextBox> x in gran)
            {
                var a = new Label();
                a.Width = 30;
                a.Content = new TextBlock().Text = c.ToString();
                a.FontSize = 10;
                gr_output.Children.Add(a);
                foreach (TextBox t in x)
                {
                    gr_output.Children.Add(t);
                }
                c++;
            }
            
        }

        private void Subopt()
        {
            int p1 = -1;
            int p2 = -1;
            int b1 = -1;
            int b2 = -1;
            int f1 = -1;
            int f2 = -1;
            int l1 = -1;
            int l2 = -1;
            int v1 = -1;
            int v2 = -1;

            
            int max = -1;
            int min = 999999999;
            int maxi = -1;
            int mini = -1;
            List<List<TextBox>> templist = new List<List<TextBox>>();


            try
            {
                int j = 0;
                foreach (List<TextBox> a in col_of_resh)
                {
                    subopt.Add(new List<TextBox>());
                    foreach (TextBox t in a)
                    {
                        TextBox temp = new TextBox();
                        temp.Width = t.Width;
                        temp.Height = t.Height;
                        temp.Margin = t.Margin;
                        temp.Text = t.Text;
                        subopt[j].Add(temp);
                    }
                    j++;
                }

            }
            catch (Exception e)
            {
                MessageBox.Show("ошибка копирования" + e.ToString());
            }

            try
            {
                if (!String.IsNullOrEmpty(price3.Text) )
                    p1 = Convert.ToInt32(price3.Text);
                if (!String.IsNullOrEmpty(price4.Text) )
                    p2 = Convert.ToInt32(price4.Text);
                if (!String.IsNullOrEmpty(bus3.Text) )
                    b1 = Convert.ToInt32(bus3.Text);
                if (!String.IsNullOrEmpty(bus4.Text) )
                    b2 = Convert.ToInt32(bus4.Text);
                if (!String.IsNullOrEmpty(frequancy3.Text) )
                    f1 = Convert.ToInt32(frequancy3.Text);
                if (!String.IsNullOrEmpty(frequancy4.Text) )
                    f2 = Convert.ToInt32(frequancy4.Text);
                if (!String.IsNullOrEmpty(lenght3.Text) )
                    l1 = Convert.ToInt32(lenght3.Text);
                if (!String.IsNullOrEmpty(lenght4.Text) )
                    l2 = Convert.ToInt32(lenght4.Text);
                if (!String.IsNullOrEmpty(volume3.Text) )
                    v1 = Convert.ToInt32(volume3.Text);
                if (!String.IsNullOrEmpty(volume4.Text) )
                    v2 = Convert.ToInt32(volume4.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Неправильно введены границы\n\n" + ex.ToString());
                return;
            }

            try
            {
                List<int> deleted_string = new List<int>();
                for (int i = 0; i < col_of_resh.Count; i++)
                {
                    switch (sub_main_param.SelectedIndex)
                    {
                        case 0:
                            if (b1 != -1)
                            {
                                if (Convert.ToInt32(subopt[i][1].Text) > b1)
                                {
                                    deleted_string.Add(i);
                                }
                            }
                            if (b2 != -1)
                            {
                                if (Convert.ToInt32(subopt[i][1].Text) < b2)
                                {
                                    deleted_string.Add(i);
                                }
                            }
                            if (f1 != -1)
                            {
                                if (Convert.ToInt32(subopt[i][2].Text) > f1)
                                {
                                    deleted_string.Add(i);
                                }
                            }
                            if (f2 != -1)
                            {
                                if (Convert.ToInt32(subopt[i][2].Text) < f2)
                                {
                                    deleted_string.Add(i);
                                }
                            }
                            if (l1 != -1)
                            {
                                if (Convert.ToInt32(subopt[i][3].Text) > l1)
                                {
                                    deleted_string.Add(i);
                                }
                            }
                            if (l2 != -1)
                            {
                                if (Convert.ToInt32(subopt[i][3].Text) < l2)
                                {
                                    deleted_string.Add(i);
                                }
                            }
                            if (v1 != -1)
                            {
                                if (Convert.ToInt32(subopt[i][4].Text) > v1)
                                {
                                    deleted_string.Add(i);
                                }
                            }
                            if (v2 != -1)
                            {
                                if (Convert.ToInt32(subopt[i][4].Text) < v2)
                                {
                                    deleted_string.Add(i);
                                }
                            }
                        break;
                        case 1:
                            if (p1 != -1)
                            {
                                if (Convert.ToInt32(subopt[i][0].Text) > p1)
                                {
                                    deleted_string.Add(i);
                                }
                            }
                            if (p2 != -1)
                            {
                                if (Convert.ToInt32(subopt[i][0].Text) < p2)
                                {
                                    deleted_string.Add(i);
                                }
                            }
                            if (f1 != -1)
                            {
                                if (Convert.ToInt32(subopt[i][2].Text) > f1)
                                {
                                    deleted_string.Add(i);
                                }
                            }
                            if (f2 != -1)
                            {
                                if (Convert.ToInt32(subopt[i][2].Text) < f2)
                                {
                                    deleted_string.Add(i);
                                }
                            }
                            if (l1 != -1)
                            {
                                if (Convert.ToInt32(subopt[i][3].Text) > l1)
                                {
                                    deleted_string.Add(i);
                                }
                            }
                            if (l2 != -1)
                            {
                                if (Convert.ToInt32(subopt[i][3].Text) < l2)
                                {
                                    deleted_string.Add(i);
                                }
                            }
                            if (v1 != -1)
                            {
                                if (Convert.ToInt32(subopt[i][4].Text) > v1)
                                {
                                    deleted_string.Add(i);
                                }
                            }
                            if (v2 != -1)
                            {
                                if (Convert.ToInt32(subopt[i][4].Text) < v2)
                                {
                                    deleted_string.Add(i);
                                }
                            }
                            break;
                        case 2:
                            if (p1 != -1)
                            {
                                if (Convert.ToInt32(subopt[i][0].Text) > p1)
                                {
                                    deleted_string.Add(i);
                                }
                            }
                            if (p2 != -1)
                            {
                                if (Convert.ToInt32(subopt[i][0].Text) < p2)
                                {
                                    deleted_string.Add(i);
                                }
                            }
                            if (b1 != -1)
                            {
                                if (Convert.ToInt32(subopt[i][1].Text) > b1)
                                {
                                    deleted_string.Add(i);
                                }
                            }
                            if (b2 != -1)
                            {
                                if (Convert.ToInt32(subopt[i][1].Text) < b2)
                                {
                                    deleted_string.Add(i);
                                }
                            }
                            if (l1 != -1)
                            {
                                if (Convert.ToInt32(subopt[i][3].Text) > l1)
                                {
                                    deleted_string.Add(i);
                                }
                            }
                            if (l2 != -1)
                            {
                                if (Convert.ToInt32(subopt[i][3].Text) < l2)
                                {
                                    deleted_string.Add(i);
                                }
                            }
                            if (v1 != -1)
                            {
                                if (Convert.ToInt32(subopt[i][4].Text) > v1)
                                {
                                    deleted_string.Add(i);
                                }
                            }
                            if (v2 != -1)
                            {
                                if (Convert.ToInt32(subopt[i][4].Text) < v2)
                                {
                                    deleted_string.Add(i);
                                }
                            }
                            break;
                        case 3:
                            if (p1 != -1)
                            {
                                if (Convert.ToInt32(subopt[i][0].Text) > p1)
                                {
                                    deleted_string.Add(i);
                                }
                            }
                            if (p2 != -1)
                            {
                                if (Convert.ToInt32(subopt[i][0].Text) < p2)
                                {
                                    deleted_string.Add(i);
                                }
                            }
                            if (b1 != -1)
                            {
                                if (Convert.ToInt32(subopt[i][1].Text) > b1)
                                {
                                    deleted_string.Add(i);
                                }
                            }
                            if (b2 != -1)
                            {
                                if (Convert.ToInt32(subopt[i][1].Text) < b2)
                                {
                                    deleted_string.Add(i);
                                }
                            }
                            if (f1 != -1)
                            {
                                if (Convert.ToInt32(subopt[i][2].Text) > f1)
                                {
                                    deleted_string.Add(i);
                                }
                            }
                            if (f2 != -1)
                            {
                                if (Convert.ToInt32(subopt[i][2].Text) < f2)
                                {
                                    deleted_string.Add(i);
                                }
                            }
                            if (v1 != -1)
                            {
                                if (Convert.ToInt32(subopt[i][4].Text) > v1)
                                {
                                    deleted_string.Add(i);
                                }
                            }
                            if (v2 != -1)
                            {
                                if (Convert.ToInt32(subopt[i][4].Text) < v2)
                                {
                                    deleted_string.Add(i);
                                }
                            }
                            break;
                        case 4:
                            if (p1 != -1)
                            {
                                if (Convert.ToInt32(subopt[i][0].Text) > p1)
                                {
                                    deleted_string.Add(i);
                                }
                            }
                            if (p2 != -1)
                            {
                                if (Convert.ToInt32(subopt[i][0].Text) < p2)
                                {
                                    deleted_string.Add(i);
                                }
                            }
                            if (b1 != -1)
                            {
                                if (Convert.ToInt32(subopt[i][1].Text) > b1)
                                {
                                    deleted_string.Add(i);
                                }
                            }
                            if (b2 != -1)
                            {
                                if (Convert.ToInt32(subopt[i][1].Text) < b2)
                                {
                                    deleted_string.Add(i);
                                }
                            }
                            if (f1 != -1)
                            {
                                if (Convert.ToInt32(subopt[i][2].Text) > f1)
                                {
                                    deleted_string.Add(i);
                                }
                            }
                            if (f2 != -1)
                            {
                                if (Convert.ToInt32(subopt[i][2].Text) < f2)
                                {
                                    deleted_string.Add(i);
                                }
                            }
                            if (l1 != -1)
                            {
                                if (Convert.ToInt32(subopt[i][3].Text) > l1)
                                {
                                    deleted_string.Add(i);
                                }
                            }
                            if (l2 != -1)
                            {
                                if (Convert.ToInt32(subopt[i][3].Text) < l2)
                                {
                                    deleted_string.Add(i);
                                }
                            }
                            break;
                    }
                }

                var a = deleted_string.Distinct().ToList();

                for (int i = a.Count - 1; i >= 0; i--)
                {
                    subopt.RemoveAt(a[i]);
                }
                
                switch ((Tendency.Children[sub_main_param.SelectedIndex] as TextBox).Text)
                {
                    case "+":
                        for(int i = 0; i< subopt.Count; i++)
                        {
                            if(Convert.ToInt32(subopt[i][sub_main_param.SelectedIndex].Text) > max)
                            {
                                maxi = Convert.ToInt32(subopt[i][sub_main_param.SelectedIndex].Text);
                                max = maxi;
                            }
                        }
                        for (int x = 0; x < subopt.Count; x++)
                        {
                            if (Convert.ToInt32(subopt[x][sub_main_param.SelectedIndex].Text) >= maxi)
                            {
                                templist.Add(subopt[x]);
                            }
                        }
                        break;
                    case "-":
                        for (int i = 0; i < subopt.Count; i++)
                        {
                            if (Convert.ToInt32(subopt[i][sub_main_param.SelectedIndex].Text) < min)
                            {
                                mini = Convert.ToInt32(subopt[i][sub_main_param.SelectedIndex].Text);
                                min = mini;
                            }
                        }
                        for (int x = 0; x < subopt.Count; x++)
                        {
                            if (Convert.ToInt32(subopt[x][sub_main_param.SelectedIndex].Text) <= mini)
                            {
                                templist.Add(subopt[x]);
                            }
                        }
                        break;
                }                
            }
            catch (Exception e)
            {
                MessageBox.Show("Ошибка обработки границ\n" + e.ToString());
            }

            int c = 1;
            foreach (List<TextBox> x in templist)
            {
                var a = new Label();
                a.Width = 30;
                a.Content = new TextBlock().Text = c.ToString();
                a.FontSize = 10;
                sub_output.Children.Add(a);
                foreach (TextBox t in x)
                {
                    sub_output.Children.Add(t);
                }
                c++;
            }
        }

        private void Lexicograph()
        {
            // После 2 нужно remove из листа templist

            int max = -1;
            int min = 999999999;
            int maxi = -1;
            int mini = -1;
            
            List<List<TextBox>> templist = new List<List<TextBox>>();

            try
            {
                int j = 0;
                foreach (List<TextBox> a in col_of_resh)
                {
                    lexan.Add(new List<TextBox>());
                    foreach (TextBox t in a)
                    {
                        TextBox temp = new TextBox();
                        temp.Width = t.Width;
                        temp.Height = t.Height;
                        temp.Margin = t.Margin;
                        temp.Text = t.Text;
                        lexan[j].Add(temp);
                    }
                    j++;
                }

            }
            catch (Exception e)
            {
                MessageBox.Show("ошибка копирования" + e.ToString());
            }

            try
            {
                if (lex_first.SelectedIndex != -1) { 

                     switch ((Tendency.Children[lex_first.SelectedIndex] as TextBox).Text)
                {
                    case "+":
                        for (int i = 0; i < lexan.Count; i++)
                        {
                            if (Convert.ToInt32(lexan[i][lex_first.SelectedIndex].Text) > max)
                            {
                                maxi = Convert.ToInt32(lexan[i][lex_first.SelectedIndex].Text);
                                max = maxi;
                            }
                        }
                        for (int x = 0; x < lexan.Count; x++)
                        {
                            if (Convert.ToInt32(lexan[x][lex_first.SelectedIndex].Text) == maxi)
                            {
                                templist.Add(lexan[x]);
                            }
                        }
                        break;
                    case "-":
                        for (int i = 0; i < lexan.Count; i++)
                        {
                            if (Convert.ToInt32(lexan[i][lex_first.SelectedIndex].Text) < min)
                            {
                                mini = Convert.ToInt32(lexan[i][lex_first.SelectedIndex].Text);
                                min = mini;
                            }
                        }
                        for (int x = 0; x < lexan.Count; x++)
                        {
                            if (Convert.ToInt32(lexan[x][lex_first.SelectedIndex].Text) == mini)
                            {
                                templist.Add(lexan[x]);
                            }
                        }
                        break;
                }

                }
                max = -1;
                min = 999999999;
                maxi = -1;
                mini = -1;

                if (lex_second.SelectedIndex != -1)
                {
                    List<int> del = new List<int>();
                    switch ((Tendency.Children[lex_second.SelectedIndex] as TextBox).Text)
                    {
                        
                        case "+":
                            for (int i = 0; i < templist.Count; i++)
                            {
                                if (Convert.ToInt32(templist[i][lex_second.SelectedIndex].Text) > max)
                                {
                                    maxi = Convert.ToInt32(templist[i][lex_second.SelectedIndex].Text);
                                    max = maxi;
                                }
                            }
                            for (int x = 0; x < templist.Count; x++)
                            {
                                if (Convert.ToInt32(templist[x][lex_second.SelectedIndex].Text) < maxi)
                                {
                                    del.Add(x);
                                }
                            }
                            for(int r = del.Count-1; r >= 0; r--)
                            {
                                templist.RemoveAt(del[r]);
                            }
                            break;
                        case "-":
                            for (int i = 0; i < templist.Count; i++)
                            {
                                if (Convert.ToInt32(templist[i][lex_second.SelectedIndex].Text) < min)
                                {
                                    mini = Convert.ToInt32(templist[i][lex_second.SelectedIndex].Text);
                                    min = mini;
                                }
                            }
                            for (int x = 0; x < templist.Count; x++)
                            {
                                if (Convert.ToInt32(templist[x][lex_second.SelectedIndex].Text) > mini)
                                {
                                    del.Add(x);
                                }
                            }
                            for (int r = del.Count - 1; r >= 0; r--)
                            {
                                templist.RemoveAt(del[r]);
                            }
                            break;
                    }
                }
                max = -1;
                min = 999999999;
                maxi = -1;
                mini = -1;

                if (lex_third.SelectedIndex != -1)
                {
                    List<int> del = new List<int>();
                    switch ((Tendency.Children[lex_third.SelectedIndex] as TextBox).Text)
                    {
                        case "+":
                            for (int i = 0; i < templist.Count; i++)
                            {
                                if (Convert.ToInt32(templist[i][lex_third.SelectedIndex].Text) > max)
                                {
                                    maxi = Convert.ToInt32(templist[i][lex_third.SelectedIndex].Text);
                                    max = maxi;
                                }
                            }
                            for (int x = 0; x < templist.Count; x++)
                            {
                                if (Convert.ToInt32(templist[x][lex_third.SelectedIndex].Text) < maxi)
                                {
                                    del.Add(x);
                                }
                            }
                            for (int r = del.Count - 1; r >= 0; r--)
                            {
                                templist.RemoveAt(del[r]);
                            }
                            break;
                        case "-":
                            for (int i = 0; i < templist.Count; i++)
                            {
                                if (Convert.ToInt32(templist[i][lex_third.SelectedIndex].Text) < min)
                                {
                                    mini = Convert.ToInt32(templist[i][lex_third.SelectedIndex].Text);
                                    min = mini;
                                }
                            }
                            for (int x = 0; x < templist.Count; x++)
                            {
                                if (Convert.ToInt32(templist[x][lex_third.SelectedIndex].Text) > mini)
                                {
                                    del.Add(x);
                                }
                            }
                            for (int r = del.Count - 1; r >= 0; r--)
                            {
                                templist.RemoveAt(del[r]);
                            }
                            break;
                    }
                }
                max = -1;
                min = 999999999;
                maxi = -1;
                mini = -1;
                if (lex_fourth.SelectedIndex != -1)
                {
                    List<int> del = new List<int>();
                    switch ((Tendency.Children[lex_fourth.SelectedIndex] as TextBox).Text)
                    {
                        case "+":
                            for (int i = 0; i < templist.Count; i++)
                            {
                                if (Convert.ToInt32(templist[i][lex_fourth.SelectedIndex].Text) > max)
                                {
                                    maxi = Convert.ToInt32(templist[i][lex_fourth.SelectedIndex].Text);
                                    max = maxi;
                                }
                            }
                            for (int x = 0; x < templist.Count; x++)
                            {
                                if (Convert.ToInt32(templist[x][lex_fourth.SelectedIndex].Text) < maxi)
                                {
                                    del.Add(x);
                                }
                            }
                            for (int r = del.Count - 1; r >= 0; r--)
                            {
                                templist.RemoveAt(del[r]);
                            }
                            break;
                        case "-":
                            for (int i = 0; i < templist.Count; i++)
                            {
                                if (Convert.ToInt32(templist[i][lex_fourth.SelectedIndex].Text) < min)
                                {
                                    mini = Convert.ToInt32(templist[i][lex_fourth.SelectedIndex].Text);
                                    min = mini;
                                }
                            }
                            for (int x = 0; x < templist.Count; x++)
                            {
                                if (Convert.ToInt32(templist[x][lex_fourth.SelectedIndex].Text) > mini)
                                {
                                    del.Add(x);
                                }
                            }
                            for (int r = del.Count - 1; r >= 0; r--)
                            {
                                templist.RemoveAt(del[r]);
                            }
                            break;
                    }
                }

                max = -1;
                min = 999999999;
                maxi = -1;
                mini = -1;

                if (lex_fiveth.SelectedIndex != -1)
                {
                    List<int> del = new List<int>();
                    switch ((Tendency.Children[lex_fiveth.SelectedIndex] as TextBox).Text)
                    {
                        case "+":
                            for (int i = 0; i < templist.Count; i++)
                            {
                                if (Convert.ToInt32(templist[i][lex_fiveth.SelectedIndex].Text) > max)
                                {
                                    maxi = Convert.ToInt32(templist[i][lex_fiveth.SelectedIndex].Text);
                                    max = maxi;
                                }
                            }
                            for (int x = 0; x < templist.Count; x++)
                            {
                                if (Convert.ToInt32(templist[x][lex_fiveth.SelectedIndex].Text) < maxi)
                                {
                                    del.Add(x);
                                }
                            }
                            for (int r = del.Count - 1; r >= 0; r--)
                            {
                                templist.RemoveAt(del[r]);
                            }
                            break;
                        case "-":
                            for (int i = 0; i < templist.Count; i++)
                            {
                                if (Convert.ToInt32(templist[i][lex_fiveth.SelectedIndex].Text) < min)
                                {
                                    mini = Convert.ToInt32(templist[i][lex_fiveth.SelectedIndex].Text);
                                    min = mini;
                                }
                            }
                            for (int x = 0; x < templist.Count; x++)
                            {
                                if (Convert.ToInt32(templist[x][lex_fiveth.SelectedIndex].Text) > mini)
                                {
                                    del.Add(x);
                                }
                            }
                            for (int r = del.Count - 1; r >= 0; r--)
                            {
                                templist.RemoveAt(del[r]);
                            }
                            break;
                    }
                }

            }
            catch (Exception e)
            {
                MessageBox.Show("Ошибка подсчета\n" + e.ToString());
            }

            int c = 1;
            foreach (List<TextBox> x in templist)
            {
                var a = new Label();
                a.Width = 30;
                a.Content = new TextBlock().Text = c.ToString();
                a.FontSize = 10;
                lex_output.Children.Add(a);
                foreach (TextBox t in x)
                {
                    lex_output.Children.Add(t);
                }
                c++;
            }




        }

        private void update_Click(object sender, RoutedEventArgs e)
        {
            int j = 1;
            foreach (List<TextBox> x in col_of_resh)
            {
                var a = new Label();
                a.Width = 30;
                a.Content = new TextBlock().Text = j.ToString();
                a.FontSize = 10;
                Par_Opt.Children.Add(a);
                j++;
                if(x.Count >4)
                foreach (TextBox t in x)
                {
                    Par_Opt.Children.Add(t);
                }
                else
                {
                    Label b = new Label();
                    b.Width = 600;
                    b.Height = 50;
                    Par_Opt.Children.Add(b);
                }
                    
            }
        }

        private void Apply_Click(object sender, RoutedEventArgs e)
        {
            apply();
        }

        private void gr_apply_Click(object sender, RoutedEventArgs e)
        {
            gran.Clear();
            gr_output.Children.Clear();
            appl_gr();
        }

        private void Sub_apply_Click(object sender, RoutedEventArgs e)
        {
            subopt.Clear();
            sub_output.Children.Clear();
            Subopt();

        }

        private void Lex_apply_Click(object sender, RoutedEventArgs e)
        {
            lexan.Clear();
            lex_output.Children.Clear();
            Lexicograph();

        }
    }
}
