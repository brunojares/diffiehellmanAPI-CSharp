using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace aplicativo
{
    public class Interlocutor : IDisposable
    {
        private ECDiffieHellmanCng _dados;
        protected ECDiffieHellmanCng dados
        {
            get
            {
                if (_dados == null)
                {
                    _dados = new ECDiffieHellmanCng();
                    _dados.KeyDerivationFunction = ECDiffieHellmanKeyDerivationFunction.Hash;
                    _dados.HashAlgorithm = CngAlgorithm.Sha256;
                }
                return _dados;
            }
        }

        private byte[] _chaveIndividual;
        public byte[] chaveIndividual
        {
            get
            {
                if (_chaveIndividual == null)
                    _chaveIndividual = dados.PublicKey.ToByteArray();
                return _chaveIndividual;
            }
        }

        public virtual void Dispose()
        {
            if (_dados != null)
            {
                _dados.Dispose();
                _dados = null;
            }
            _chaveIndividual = null;
        }
    }
}
