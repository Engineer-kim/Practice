using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BookManager
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            label5.Text = DataManager.Books.Count.ToString();
            label6.Text = DataManager.Users.Count.ToString();
            label7.Text = DataManager.Books.Where((x) => x.isBorrowed).Count().ToString();
            label8.Text = DataManager.Books.
                Where((x) => { return x.isBorrowed && x.BorrowdAt.AddDays(7) < DateTime.Now; }).Count().ToString();

            dataGridView2.DataSource = DataManager.Books;
            dataGridView3.DataSource = DataManager.Users;

            dataGridView2.CurrentCellChanged += DataGridView2_CurrentCellChanged;
            dataGridView3.CurrentCellChanged += DataGridView3_CurrentCellChanged;

            button1.Click += Button1_Click;
            button2.Click += Button2_Click;
        }

        private void Button2_Click(object sender, EventArgs e) //반납
        {
            if (textBox1.Text.Trim() == "")
            {
                MessageBox.Show("도서를 먼저 입력하시오");
            }
            else
            {
                try
                {
                    Book book = DataManager.Books.Single((x) => x.ISbn == textBox1.Text);


                    if (book.isBorrowed)
                    {
                        book.UserId = 0;
                        book.UserName = "";
                        book.isBorrowed = false;
                        // book.BorrowdAt = new DateTime();

                        dataGridView2.DataSource = null;
                        dataGridView2.DataSource = DataManager.Books;

                        if (book.BorrowdAt.AddDays(7) < DateTime.Now)
                            MessageBox.Show(book.Name + "이 연채 상태로 반납되었습니다");
                        else
                            MessageBox.Show(book.Name + "이  반납되었습니다");
                    }
                    else
                   
                        MessageBox.Show("대여상태 아님");
                    }catch(Exception ex)
                {
                    MessageBox.Show("존재하지 않는 도서이다");
                }

                }
        }

        private void Button1_Click(object sender, EventArgs e) //대여
        {
          if(textBox1.Text.Trim() == "")
            {
                MessageBox.Show("도서 선택 먼저 선택하시오");
            }
          else if(textBox3.Text.Trim() == "")
            {
                MessageBox.Show("사용자를 선택하시오");
            }
            else 
            {
                try
                {
                    Book book = DataManager.Books.Single((x) => x.ISbn == textBox1.Text);
                    if (book.isBorrowed)
                        MessageBox.Show("이미 대여중인 도서입니다");
                    else
                    {
                        User user = DataManager.Users.Single((x) => x.Id.ToString() == textBox3.Text);
                        book.UserId = user.Id;
                        book.UserName = user.Name;
                        book.isBorrowed = true;
                        book.BorrowdAt = DateTime.Now;

                        dataGridView2.DataSource = null;
                        dataGridView2.DataSource = DataManager.Books;

                        MessageBox.Show(book.Name + "이/가" + user.Name + "님계 대여되었습니다 ");
                    }
                }catch(Exception exception)
                {
                    MessageBox.Show("존재하지 않는 도서또는 사용자입니다");
                }
            }
        }

        private void DataGridView3_CurrentCellChanged(object sender, EventArgs e)//사용자
        {
            try
            {
                User user = dataGridView3.CurrentRow.DataBoundItem as User;
                textBox3.Text = user.Id.ToString();

            }catch(Exception exception)
            {

            }
        }

        private void DataGridView2_CurrentCellChanged(object sender, EventArgs e)// 도서현황
        {
            try
            {
                Book book = dataGridView2.CurrentRow.DataBoundItem as Book;
                textBox1.Text = book.ISbn;
                textBox2.Text = book.Name;
            }catch(Exception exception)
            {

            }
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e) 
        {

        }

        private void groupBox4_Enter(object sender, EventArgs e)
        {

        }

        private void dataGridView3_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

      //  private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
     //   {
     //       new Form2().ShowDialog(); 
     //   }
        private void menuStrip2_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            new Form2().ShowDialog();  
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void toolStripMenuItem5_Click(object sender, EventArgs e)
        {
            new Form2().ShowDialog();
        }

        private void toolStripMenuItem6_Click(object sender, EventArgs e)
        {
            new Form3().ShowDialog();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

       }
    }
}
