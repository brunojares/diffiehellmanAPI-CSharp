using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace aplicativo
{
    public class Emissor : Interlocutor, IEmissor
    {
        public byte[][] envia(IReceptor receptor, string mensagem)
        {
            var _chavePrivada = this.dados.DeriveKeyMaterial(CngKey.Import(receptor.chaveIndividual, CngKeyBlobFormat.EccPublicBlob));
            //...
            using (Aes aes = new AesCryptoServiceProvider())
            {
                aes.Key = _chavePrivada;
                //iv = aes.IV;

                // Encrypt the message
                using (MemoryStream ciphertext = new MemoryStream())
                using (CryptoStream cs = new CryptoStream(ciphertext, aes.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    byte[] plaintextMessage = Encoding.UTF8.GetBytes(mensagem);
                    cs.Write(plaintextMessage, 0, plaintextMessage.Length);
                    cs.Close();
                    //encryptedMessage = ciphertext.ToArray();
                    return new byte[][]{
                        aes.IV,
                        ciphertext.ToArray()
                    };
                }
            }
        }
    }
}
