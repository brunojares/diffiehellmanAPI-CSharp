using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace aplicativo
{
    public class Receptor : Interlocutor, IReceptor
    {
        public string recebe(IEmissor emissor, byte[][] mensagem)
        {
            if ((mensagem != null) && (mensagem.Length == 2))
            {
                using (Aes aes = new AesCryptoServiceProvider())
                {
                    aes.Key = dados.DeriveKeyMaterial(CngKey.Import(emissor.chaveIndividual, CngKeyBlobFormat.EccPublicBlob));
                    aes.IV = mensagem[0];
                    // Decrypt the message
                    using (MemoryStream plaintext = new MemoryStream())
                    {
                        using (CryptoStream cs = new CryptoStream(plaintext, aes.CreateDecryptor(), CryptoStreamMode.Write))
                        {
                            cs.Write(mensagem[1], 0, mensagem[1].Length);
                            cs.Close();
                            return Encoding.UTF8.GetString(plaintext.ToArray());
                        }
                    }
                }
            }
            else
                throw new InvalidDataException("[0] Mensagem corronpida");
        }
    }
}
