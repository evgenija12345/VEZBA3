﻿using System;
using System.Text;
using System.Windows.Forms;
using System.Security.Cryptography;

namespace VEZBA3
{
    public partial class Form1 : Form
    {
        UTF32Encoding ByteConverter = new UTF32Encoding();
        RSACryptoServiceProvider RSA = new RSACryptoServiceProvider();
        public Form1()
        {
            InitializeComponent();
        }
        static public byte[] RSAEncrypt(byte[] DataToEncrypt, RSAParameters RSAKeyInfo, bool DoOAEPPadding)
        {
            try
            {
                byte[] encryptedData;
                using (RSACryptoServiceProvider RSA = new RSACryptoServiceProvider())
                {
                    RSA.ImportParameters(RSAKeyInfo);
                    encryptedData = RSA.Encrypt(DataToEncrypt, DoOAEPPadding);
                }
                return encryptedData;
            }
            catch (CryptographicException e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }
        private void btnEncrypt_Click(object sender, EventArgs e)
        {
            try
            {
                RSACryptoServiceProvider RSA = new RSACryptoServiceProvider();
                RSA.FromXmlString(txtPublic.Text);
                byte[] decrypted = System.Text.Encoding.Unicode.GetBytes(txtPlain.Text);
                byte[] encrypted = RSA.Encrypt(decrypted, false);
                txtEncrypt.Text = System.Convert.ToBase64String(encrypted);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        static public byte[] RSADecrypt(byte[] DataToDecrypt, RSAParameters RSAKeyInfo, bool DoOAEPPadding)
        {
            try
            {
                byte[] decryptedData;
                using (RSACryptoServiceProvider RSA = new RSACryptoServiceProvider())
                {
                    RSA.ImportParameters(RSAKeyInfo);
                    decryptedData = RSA.Decrypt(DataToDecrypt, DoOAEPPadding);
                }
                return decryptedData;
            }
            catch (CryptographicException e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }
        private void btnDecrypt_Click(object sender, EventArgs e)
        {
            if (txtDecMsg.Text == "")
            {
                MessageBox.Show("Prazen Vlez", "Greska", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                try
                {
                    CspParameters cspParam = new CspParameters();
                    cspParam.Flags = CspProviderFlags.UseMachineKeyStore;
                    RSACryptoServiceProvider RSA = new RSACryptoServiceProvider(cspParam);
                    RSA.FromXmlString(txtPrivate.Text);
                    byte[] encrypted = System.Convert.FromBase64String(txtDecMsg.Text);
                    byte[] decrypted = RSA.Decrypt(encrypted, false);
                    txtDecrypt.Text = System.Text.Encoding.Unicode.GetString(decrypted);
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
    }
}
