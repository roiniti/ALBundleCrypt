using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ALBundleCrypt
{
    class DigitalSea
    {
        static readonly byte[] key_bytes =
        {
            0xBC, 0x4B, 0x77, 0x1D, 0xE8, 0xC0, 0x96, 0x00, 0x8C, 0xA2, 0x69, 0x00, 0xB9, 0xCB, 0xDA, 0x35,
            0xC2, 0xCE, 0x63, 0x00, 0xA5, 0x6B, 0x5A, 0x00, 0x2E, 0x71, 0x92, 0x14, 0x19, 0xDE, 0x92, 0x00,
            0x93, 0x62, 0x0B, 0x00, 0x46, 0x56, 0xDF, 0x03, 0xC2, 0x63, 0x50, 0x00, 0xD5, 0x93, 0x72, 0x00,
            0xA3, 0x5B, 0x5E, 0x00, 0xCB, 0xAF, 0xC3, 0x30, 0x76, 0xA6, 0x1C, 0x01, 0xAD, 0x4E, 0x78, 0x00,
            0x02, 0xB9, 0x4B, 0x00, 0x8C, 0xF4, 0x49, 0x00, 0x54, 0x39, 0x81, 0x05
        };







        public static byte[] Scipio(byte[] arrayIn)
        {
            //Useless buffer for the wrapper?
            byte[] pGcBuffer = null;
            //The last 4 bytes are the size of the wrapper
            int wrapper_size = (arrayIn[arrayIn.Length - 1] + (arrayIn[arrayIn.Length - 2] << 8)) | (arrayIn[arrayIn.Length - 3] << 16) | (arrayIn[arrayIn.Length - 4] << 24);
            byte[] dec_buf = new byte[arrayIn.Length - 4 - wrapper_size];
            //Copy the bytes of the decode key to a lis of uints
            uint[] dec_key = new uint[key_bytes.Length / 4];
            for (int i = 0; i < key_bytes.Length; i += 4)
                dec_key[i / 4] = BitConverter.ToUInt32(key_bytes, i);
            if (wrapper_size <= 0)
            {
                pGcBuffer = null;
            }
            else
            {
                //TODO interpret and save the wrapper for encript
                /*
                pGcBuffer = new byte[wrapper_size];
                int wrapper_start = arrayIn.Length - wrapper_size - 4;//todo replace mem dir el +12??
                int enc_data_size = dec_buf.Length;
                //Coyp the wrapper to the pGcBuffer
                for(int i = 0; enc_data_size < arrayIn.Length - 4; enc_data_size++)
                {
                    pGcBuffer[i] = arrayIn[wrapper_start + i];
                    i++;
                }
                //Decodes the wrapper
                int v29 = 235;
                for (int i = 0; i < wrapper_size; ++i)
                {
                    int v193 = v29;
                    int dec_i = pGcBuffer[i] ^ (v193 >> 8);
                    int v38 = v193 + pGcBuffer[i];
                    int v41 = 205 * v38;
                    v29 = v41 + 207;
                    pGcBuffer[i] = (byte)dec_i;
                }
                */
                /*
                //return pGcBuffer;
                */
                int[] crypt_buffer = new int[2];
                uint current_index = 0;
                uint contador_4 = 0;
                if (dec_buf.Length != 8)
                {
                    do
                    {
                        //Joins the first 4 bytes of index onto a single int
                        crypt_buffer[0] = (arrayIn[current_index] + (arrayIn[current_index + 1] << 8)) |
                            (arrayIn[current_index + 2] << 16) | (arrayIn[current_index + 3] << 24);
                        //Joins the second 4 bytes of index onto a single int
                        crypt_buffer[1] = (arrayIn[current_index + 4] + (arrayIn[current_index + 5] << 8)) |
                            (arrayIn[current_index + 6] << 16) | (arrayIn[current_index + 7] << 24);
                        //Redundand variables only for better visualization
                        //Stores the initial value of the ints
                        uint dec_1 = (uint)crypt_buffer[0];
                        uint dec_2 = (uint)crypt_buffer[1];

                        //test(crypt_buffer, current_index, dec_key);
                        //Decrypt the int based on the position
                        switch (contador_4)
                        {
                            case 0:
                                for (uint v83 = 234846730; v83 != 0; v83 -= 117423365)
                                {



                                    dec_1 = (uint)crypt_buffer[0];
                                    //osciles 2 and 1
                                    int v189 = (int)((v83 >> 2) % 4);
                                    //osciles 3 and 0
                                    int v197 = (v189 ^ 1) % 4;
                                    crypt_buffer[1] -= (int)
                                        (((((dec_1 >> 5) ^ 4 * dec_1) + ((dec_1 >> 3) ^ 16 * dec_1)) ^ ((v83 ^ dec_1)
                                            + ((dec_key[v197]) ^ dec_1))));

                                    dec_2 = (uint)crypt_buffer[1];
                                    crypt_buffer[0] -= (int)
                                        (((((dec_2 >> 5) ^ 4 * dec_2) + ((dec_2 >> 3) ^ 16 * dec_2)) ^ ((v83 ^ dec_2)
                                            + (dec_key[v189] ^ dec_2))));
                                    dec_1 = (uint)crypt_buffer[0];


                                    /*
                                    //osciles 2, 1
                                    uint key_pos1 = ((v83 >> 2) % 4);
                                    //osciles 3, 0
                                    uint key_pos2 = (key_pos1 - 1) % 4;
                                    dec_2 -=(((dec_1 >> 5) ^ 4 * dec_1) + ((dec_1 >> 3) ^ 16 * dec_1)) ^ ((v83 ^ dec_1)
                                            + (dec_key[key_pos2] ^ dec_1));
                                    dec_1 -=(((dec_2 >> 5) ^ 4 * dec_2) + ((dec_2 >> 3) ^ 16 * dec_2)) ^ ((v83 ^ dec_2)
                                            + (dec_key[key_pos1] ^ dec_2));
                                */



                                }
                                break;
                            case 1:
                                for (uint v77 = 234846730; v77 != 0; v77 -= 117423365)
                                {
                                    dec_2 -= (dec_key[2] + 16 * dec_1) ^ (v77 + dec_1) ^ (dec_key[3] + (dec_1 >> 5));
                                    dec_1 -= (dec_key[0] + 16 * dec_2) ^ (v77 + dec_2) ^ (dec_key[1] + (dec_2 >> 5));
                                }
                                break;
                            case 2:
                                dec_1 = (dec_key[13] ^ current_index ^ dec_1);
                                dec_2 = (dec_key[2] ^ current_index ^ dec_2);
                                break;
                            case 3:
                                uint key_pos = 1;
                                for (uint v105 = 234846730; v105 != 0; v105 -= 117423365)
                                {
                                    dec_2 -= (dec_1 + (16 * dec_1 ^ (dec_1 >> 5))) ^ (v105 + dec_key[3]);
                                    dec_1 -= (dec_2 + (16 * dec_2 ^ (dec_2 >> 5))) ^ (v105 + dec_key[(key_pos--)] - 117423365);
                                }
                                break;
                        }
                        //Saves the values to the buffer
                        crypt_buffer[0] = (int)dec_1;
                        crypt_buffer[1] = (int)dec_2;
                        //Saves the ints as bytes
                        dec_buf[current_index + 0] = (byte)(crypt_buffer[0] >> 0);
                        dec_buf[current_index + 1] = (byte)(crypt_buffer[0] >> 8);
                        dec_buf[current_index + 2] = (byte)(crypt_buffer[0] >> 16);
                        dec_buf[current_index + 3] = (byte)(crypt_buffer[0] >> 24);
                        dec_buf[current_index + 4] = (byte)(crypt_buffer[1] >> 0);
                        dec_buf[current_index + 5] = (byte)(crypt_buffer[1] >> 8);
                        dec_buf[current_index + 6] = (byte)(crypt_buffer[1] >> 16);
                        dec_buf[current_index + 7] = (byte)(crypt_buffer[1] >> 24);
                        //Increase the counter
                        contador_4 = (contador_4 + 1) % 4;
                        current_index += 8;
                    } while (current_index < dec_buf.Length - 8);
                }
                //Copy the rest of the file if there is some left
                for (; current_index < dec_buf.Length;current_index++)
                {
                    dec_buf[current_index] = arrayIn[current_index];

                }

            }
            return dec_buf;
        }




        public static byte[] XANA(byte[] arrayIn)
        {
            byte[] pGcBuffer = null;
            int wrapper_size = 6072;
            byte[] enc_buf = new byte[arrayIn.Length + 4 + wrapper_size];
            //Copy the bytes of the decode key to a list of uints
            uint[] dec_key = new uint[key_bytes.Length / 4];
            for (int i = 0; i < key_bytes.Length; i += 4)
                dec_key[i / 4] = BitConverter.ToUInt32(key_bytes, i);

            uint current_index = (uint)enc_buf.Length;


            //Creates a wrapper filled with 0 bytes
            //TODO fill the wrapper with valid data
            pGcBuffer = new byte[wrapper_size];
            int wrapper_start = arrayIn.Length;
            
            //TODO do the encode wrapper code (this is the code for decoding the wrapper)
            int v29 = 235;
            for (int i = 0; i < wrapper_size; ++i)
            {
                int v193 = v29;
                int dec_i = pGcBuffer[i] ^ (v193 >> 8);
                int v38 = v193 + pGcBuffer[i];
                int v41 = 205 * v38;
                v29 = v41 + 207;
                pGcBuffer[i] = (byte)dec_i;
            }
            //Copy the size of the wrapper to the encoded data
            enc_buf[current_index - 1] = (byte)(wrapper_size >> 0);
            enc_buf[current_index - 2] = (byte)(wrapper_size >> 8);
            enc_buf[current_index - 3] = (byte)(wrapper_size >> 16);
            enc_buf[current_index - 4] = (byte)(wrapper_size >> 24);
            current_index = (uint)wrapper_start;

            //Copy the the wrapper to the encoded data
            for (int i = 0; current_index < enc_buf.Length-4; current_index++)
            {
                pGcBuffer[i] = enc_buf[wrapper_start + i];
                i++;
            }
            current_index = (uint)arrayIn.Length;
            current_index -= (uint)arrayIn.Length % 8;
            //Copy the raw bytes that are not in a 8 byte set
            for (int i = 0; i < arrayIn.Length % 8; i++)
            {
                enc_buf[current_index] = arrayIn[(current_index)];
                current_index++;

            }


            int[] crypt_buffer = new int[2];
            uint contador_4 = 0;
            current_index = 0;
            //Start the encoding
            do
            {
                //Joins the first 4 bytes of index onto a single int
                crypt_buffer[0] = (arrayIn[current_index] + (arrayIn[current_index + 1] << 8)) |
                    (arrayIn[current_index + 2] << 16) | (arrayIn[current_index + 3] << 24);
                //Joins the second 4 bytes of index onto a single int
                crypt_buffer[1] = (arrayIn[current_index + 4] + (arrayIn[current_index + 5] << 8)) |
                    (arrayIn[current_index + 6] << 16) | (arrayIn[current_index + 7] << 24);
                //Redundand variables only for better visualization
                //Stores the initial value of the ints
                uint dec_1 = (uint)crypt_buffer[0];
                uint dec_2 = (uint)crypt_buffer[1];
                //Encrypt the int based on the position
                switch (contador_4)
                {
                    case 0:

                        for (uint v83 = 117423365; v83 <= 234846730; v83 += 117423365)
                        {

                            //oscila entre 2 y 1
                            uint key_pos1 = ((v83 >> 2) % 4);
                            //oscila entre 3 y 0
                            uint key_pos2 = (key_pos1 ^ 1) % 4;
                            dec_2 = (uint)crypt_buffer[1];
                            crypt_buffer[0] += (int)((((dec_2 >> 5) ^ 4 * dec_2) + ((dec_2 >> 3) ^ 16 * dec_2)) ^ ((v83 ^ dec_2) + (dec_key[key_pos1] ^ dec_2)));
                            dec_1 = (uint)crypt_buffer[0];
                            crypt_buffer[1] += (int)((((dec_1 >> 5) ^ 4 * dec_1) + ((dec_1 >> 3) ^ 16 * dec_1)) ^ ((v83 ^ dec_1) + ((dec_key[key_pos2]) ^ dec_1)));
                            dec_2 = (uint)crypt_buffer[1];
                        }

                        break;
                    case 1:
                        for (uint v77 = 117423365; v77 <= 234846730; v77 += 117423365)
                        {
                            dec_1 += (dec_key[0] + 16 * dec_2) ^ (v77 + dec_2) ^ (dec_key[1] + (dec_2 >> 5));
                            dec_2 += (dec_key[2] + 16 * dec_1) ^ (v77 + dec_1) ^ (dec_key[3] + (dec_1 >> 5));
                        }

                        break;
                    case 2:
                        dec_1 = (dec_key[13] ^ current_index ^ dec_1);
                        dec_2 = (dec_key[2] ^ current_index ^ dec_2);
                        break;
                    case 3:
                        uint key_pos = 0;
                        for (uint v105 = 117423365; v105 <= 234846730; v105 += 117423365)
                        {
                            dec_1 += (dec_2 + (16 * dec_2 ^ (dec_2 >> 5))) ^ (v105 + (dec_key[(key_pos++)]) - 117423365);
                            dec_2 += (dec_1 + (16 * dec_1 ^ (dec_1 >> 5))) ^ (v105 + (dec_key[3]));
                        }
                        break;
                }
                //Saves the values to the buffer
                crypt_buffer[0] = (int)dec_1;
                crypt_buffer[1] = (int)dec_2;
                //Saves the ints as bytes
                enc_buf[current_index + 0] = (byte)(crypt_buffer[0] >> 0);
                enc_buf[current_index + 1] = (byte)(crypt_buffer[0] >> 8);
                enc_buf[current_index + 2] = (byte)(crypt_buffer[0] >> 16);
                enc_buf[current_index + 3] = (byte)(crypt_buffer[0] >> 24);
                enc_buf[current_index + 4] = (byte)(crypt_buffer[1] >> 0);
                enc_buf[current_index + 5] = (byte)(crypt_buffer[1] >> 8);
                enc_buf[current_index + 6] = (byte)(crypt_buffer[1] >> 16);
                enc_buf[current_index + 7] = (byte)(crypt_buffer[1] >> 24);
                //Increase the counter
                contador_4 = (contador_4 + 1) % 4;
                current_index += 8;
            } while (current_index < arrayIn.Length - 8);
            return enc_buf;
        }

    }
}
