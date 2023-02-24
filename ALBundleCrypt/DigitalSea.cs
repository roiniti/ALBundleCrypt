using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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


            byte[] pGcBuffer = null;

            //los ultimos 4 bytes definen el tamaño del wrapper
            //Wrapper -> cabecera de unity?
            int wrapper_size = (arrayIn[arrayIn.Length - 1] + (arrayIn[arrayIn.Length - 2] << 8)) | (arrayIn[arrayIn.Length - 3] << 16) | (arrayIn[arrayIn.Length - 4] << 24);
            //sobra el 4? nono no sobra, es de las 4 ultimas posiciones
            byte[] dec_buf = new byte[arrayIn.Length - 4 - wrapper_size];


            int[] dec_key = new int[key_bytes.Length / 4];
            for (int i = 0; i < key_bytes.Length; i += 4)
                dec_key[i / 4] = BitConverter.ToInt32(key_bytes, i);

            if (wrapper_size <= 0)
            {
                pGcBuffer = null;
            }
            else
            {
                //codigo inutil?
                /*
                pGcBuffer = new byte[wrapper_size];


                int wrapper_start = arrayIn.Length - wrapper_size - 4;//todo replace mem dir el +12??
                int enc_data_size = dec_buf.Length;
                //Se copia el wrapper al pGcBuffer
                
                for(int i = 0; enc_data_size < arrayIn.Length - 4; enc_data_size++)
                {
                    pGcBuffer[i] = arrayIn[wrapper_start + i];
                    i++;
                }

                //Hace algo con los bytes del wrapper copiados del GcBuffer
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

                //return pGcBuffer;
                */
                int[] crypt_buffer = new int[2];


                int current_index = 0;
                int contador_4 = 0;
                if (dec_buf.Length != 8)
                {
                    do
                    {
                        int v54 = (arrayIn[current_index] + (arrayIn[current_index + 1] << 8)) |
                            (arrayIn[current_index + 2] << 16) | (arrayIn[current_index + 3] << 24);
                        crypt_buffer[0] = v54;

                        int v63 = (arrayIn[current_index + 4] + (arrayIn[current_index + 5] << 8)) |
                            (arrayIn[current_index + 6] << 16) | (arrayIn[current_index + 7] << 24);
                        crypt_buffer[1] = v63;

                        uint decb_1 = 0;
                        uint decb_2 = 0;

                        switch (contador_4)
                        {
                            case 0:
                                for (int v83 = 234846730; v83 > 0; v83 -= 117423365)
                                {
                                    //valor del byte 0
                                    decb_1 = (uint)crypt_buffer[0];
                                    //oscila entre 2 y 1
                                    int v189 = ((v83 >> 2) % 4);
                                    //oscila entre 3 y 0
                                    int v197 = (v189 ^ 1) % 4;
                                    int v93 = (int)
                                        (crypt_buffer[1]
                                        - ((((decb_1 >> 5) ^ 4 * decb_1) + ((decb_1 >> 3) ^ 16 * decb_1)) ^ ((v83 ^ decb_1)
                                            + ((dec_key[v197]) ^ decb_1))));
                                    crypt_buffer[1] = v93;
                                    //valor del byte 1
                                    decb_2 = (uint)crypt_buffer[1];
                                    int v100 = (int)
                                        ((crypt_buffer[0])
                                        - ((((decb_2 >> 5) ^ 4 * decb_2) + ((decb_2 >> 3) ^ 16 * decb_2)) ^ ((v83 ^ decb_2)
                                            + (dec_key[v189] ^ decb_2))));
                                    crypt_buffer[0] = v100;
                                }
                                break;
                            case 1:


                                //Guarda el valor inicial de los bytes
                                decb_1 = ((uint)crypt_buffer[0]);
                                decb_2 = ((uint)crypt_buffer[1]);

                                uint key1 = (uint)dec_key[0];
                                uint key2 = (uint)dec_key[1];
                                uint key3 = (uint)dec_key[2];
                                uint key4 = (uint)dec_key[3];
                                for (uint v77 = 234846730; v77 != 0; v77 -= 117423365)
                                {
                                    decb_2 -= (key3 + 16 * decb_1) ^ (v77 + decb_1) ^ (key4 + (decb_1 >> 5));
                                    decb_1 -= (key1 + 16 * decb_2) ^ (v77 + decb_2) ^ (key2 + (decb_2 >> 5));
                                }
                                crypt_buffer[0] = (int)decb_1;
                                crypt_buffer[1] = (int)decb_2;
                                break;
                            case 2:

                                crypt_buffer[0] = (dec_key[13] ^ current_index ^ crypt_buffer[0]);
                                crypt_buffer[1] = (dec_key[2] ^ current_index ^ crypt_buffer[1]);
                                break;
                            case 3:


                                //Guarda el valor inicial de los bytes
                                decb_1 = (uint)crypt_buffer[0];
                                decb_2 = (uint)crypt_buffer[1];

                                //Desencripta los bytes
                                uint v106 = 5;
                                for (uint v105 = 234846730; v105 != 0; v105 -= 117423365)
                                {
                                    uint v198 = decb_2;
                                    decb_2 = (uint)(v198 - ((decb_1 + (16 * decb_1 ^ (decb_1 >> 5))) ^ (v105 + (dec_key[3]))));
                                    decb_1 -= (uint)((decb_2 + (16 * decb_2 ^ (decb_2 >> 5))) ^ (v105 + dec_key[(v106--) - 4] - 117423365));
                                }

                                //Guarda los bytes desencriptados
                                crypt_buffer[0] = (int)decb_1;
                                crypt_buffer[1] = (int)decb_2;

                                break;
                        }


                        //Guarda el numero 184[0] en el array
                        dec_buf[current_index] = (byte)(crypt_buffer[0] >> 0);
                        dec_buf[current_index + 1] = (byte)(crypt_buffer[0] >> 8);
                        dec_buf[current_index + 2] = (byte)(crypt_buffer[0] >> 16);
                        dec_buf[current_index + 3] = (byte)(crypt_buffer[0] >> 24);
                        //Guarda el numero 184[1] en el array
                        dec_buf[current_index + 4] = (byte)(crypt_buffer[1] >> 0);
                        dec_buf[current_index + 5] = (byte)(crypt_buffer[1] >> 8);
                        dec_buf[current_index + 6] = (byte)(crypt_buffer[1] >> 16);
                        dec_buf[current_index + 7] = (byte)(crypt_buffer[1] >> 24);

                        //Incrementamos el contador
                        contador_4 = (contador_4 + 1) % 4;
                        current_index += 8;
                    } while (current_index < dec_buf.Length - 8);
                }
                if (current_index < dec_buf.Length)
                {
                    do
                    {
                        SByte v164 = (SByte)arrayIn[current_index];
                        dec_buf[current_index++] = (byte)v164;

                    } while (current_index < dec_buf.Length);
                }
            }
            return dec_buf;
        }
    }
}
