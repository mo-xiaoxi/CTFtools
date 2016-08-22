#!/usr/bin/python
# -*- coding: utf-8 -*-

"""
----------------------------------------------------------------------------
"THE BEER-WARE LICENSE" (Revision 42):
ganapati (@G4N4P4T1) wrote this file. As long as you retain this notice you
can do whatever you want with this stuff. If we meet some day, and you think
this stuff is worth it, you can buy me a beer in return.
----------------------------------------------------------------------------
"""

from Crypto.PublicKey import RSA
from wiener_attack import WienerAttack
import gmpy
from libnum import *
import requests
import re
import argparse
from base64 import b64decode


class FactorizationError(Exception):
    pass


class PublicKey(object):
    def __init__(self, key):
        """Create RSA key from input content
           :param key: public key file content
           :type key: string
        """
        pub = RSA.importKey(key)
        self.n = pub.n
        self.e = pub.e
        self.key = key

    def prime_factors(self):
        """Factorize n using factordb.com
        """
        try:
            url_1 = 'http://www.factordb.com/index.php?query=%i'
            url_2 = 'http://www.factordb.com/index.php?id=%s'
            r = requests.get(url_1 % self.n)
            regex = re.compile("index\.php\?id\=([0-9]+)", re.IGNORECASE)
            ids = regex.findall(r.text)
            p_id = ids[1]
            q_id = ids[2]
            regex = re.compile("value=\"([0-9]+)\"", re.IGNORECASE)
            r_1 = requests.get(url_2 % p_id)
            r_2 = requests.get(url_2 % q_id)
            self.p = int(regex.findall(r_1.text)[0])
            self.q = int(regex.findall(r_2.text)[0])
            if self.p == self.q == self.n:
                raise FactorizationError()
        except:
            raise FactorizationError()

    def __str__(self):
        """Print armored public key
        """
        return self.key


class PrivateKey(object):
    def __init__(self, p, q, e, n):
        """Create private key from base components
           :param p: extracted from n
           :type p: int
           :param q: extracted from n
           :type q: int
           :param e: exponent
           :type e: int
           :param n: n from public key
           :type n: int
        """
        t = (p-1)*(q-1)
        d = self.find_inverse(e, t)
        self.key = RSA.construct((n, e, d, p, q))

    def decrypt(self, cipher):
        """Uncipher data with private key
           :param cipher: input cipher
           :type cipher: string
        """
        return self.key.decrypt(cipher)

    def __str__(self):
        """Print armored private key
        """
        return self.key.exportKey()

    def eea(self, a, b):
        if b == 0:
            return (1, 0)
        (q, r) = (a//b, a % b)
        (s, t) = self.eea(b, r)
        return (t, s-(q * t))

    def find_inverse(self, x, y):
        inv = self.eea(x, y)[0]
        if inv < 1:
            inv += y
        return inv


if __name__ == "__main__":
    """Main method (entrypoint)
    """
    parser = argparse.ArgumentParser(description='RSA CTF Tool')
    parser.add_argument('--pkey',
                        dest='public_key',
                        help='public key file',
                        default=None)
    parser.add_argument('--pqne',
                        dest='pqne',
                        help='input: p,q,n,e',
                        default=None)
    parser.add_argument('--crypto',
                        dest='uncipher',
                        help='uncipher a file',
                        default=None)
    parser.add_argument('--format',
                        dest='format',
                        help='uncipher file format hex, base64 or num，default char',
                        default='char')
    parser.add_argument('--verbose',
                        dest='verbose',
                        help='verbose mode (display n, e, p and q)',
                        action='store_true')
    parser.add_argument('--pri',
                        dest='private',
                        help='Display private key if recovered',
                        action='store_true')

    args = parser.parse_args()

    # Open cipher file
    unciphered = None
    if args.uncipher is not None:
        cipher = open(args.uncipher, 'r').read().strip()
        if args.format == 'num':
            cipher = n2s(int(cipher))
        elif args.format == 'hex':
            cipher = int(cipher,16)
        elif args.format == 'base64':
            cipher = b64decode(cipher)

    priv_key = None
    if args.pqne is None:
        # Load public key
        try:
            key = open(args.public_key, 'r').read()
            pub_key = PublicKey(key)
        except Exception as e:
            if args.verbose:
                print "publickey error",e
                print "Crack with p,q"

        print '"n" is:' + str(pub_key.n)
        print "*"*60
        print '"e" is:' + str(pub_key.e)
        print "*"*60

        # Hastad's attack
        if pub_key.e == 3 and args.uncipher is not None:
            if args.verbose:
                print "Try Hastad's attack"

            orig = s2n(cipher)
            c = orig
            while True:
                m = gmpy.root(c, 3)[0]
                if pow(m, 3, pub_key.n) == orig:
                    unciphered = n2s(m)
                    break
                c += pub_key.n
        else:
            if args.verbose:
                print "Try weak key attack"
            try:
                pub_key.prime_factors()
                priv_key = PrivateKey(long(pub_key.p),
                                      long(pub_key.q),
                                      long(pub_key.e),
                                      long(pub_key.n))

                if args.uncipher is not None:
                    unciphered = priv_key.decrypt(cipher)
            except FactorizationError:
                unciphered = None

        if unciphered is None and priv_key is None:
            if args.verbose:
                print "Try Wiener's attack"

            # Wiener's attack
            wiener = WienerAttack(pub_key.n,pub_key.e)
            if wiener.p is not None and wiener.q is not None:
                pub_key.p = wiener.p
                pub_key.q = wiener.q
                priv_key = PrivateKey(long(pub_key.p),
                                      long(pub_key.q),
                                      long(pub_key.e),
                                      long(pub_key.n))

                if args.uncipher is not None:
                    unciphered = priv_key.decrypt(cipher)
    # user define p,q,e,n
    elif unciphered is None and priv_key is None:
        if args.verbose:
            print "Crack with p,q"

        pqne_list = args.pqne.split(',')
        nump = long(pqne_list[0])
        numq = long(pqne_list[1])
        nume = long(pqne_list[3])
        numn = long(pqne_list[2])
        if nume == 3 and args.uncipher is not None:
            if args.verbose:
                print "Try Hastad's attack"

            orig = cipher
            c = orig
            while True:
                m = gmpy.root(c, 3)[0]
                if pow(m, 3, numn) == orig:
                    unciphered = n2s(m)
                    break
                c += numn
        else:
            priv_key = PrivateKey(nump,numq,nume,numn)

            if args.uncipher is not None:
                unciphered = priv_key.decrypt(cipher)

    if priv_key is not None and args.private:
        print priv_key

    if unciphered is not None and args.uncipher is not None:
        print "*"*60
        print "Clear text : %s" % unciphered
        print "*"*60
        print "Clear text in number: %s" % s2n(unciphered)
    else:
        if args.uncipher is not None:
            print "Sorry, cracking failed"
