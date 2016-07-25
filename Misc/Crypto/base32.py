#!/usr/bin/env python
# Copyright (C) 2013 thuxnder <patrick@bluebox.com>
#
# Licensed under the Apache License, Version 2.0 (the 'License');
# you may not use this file except in compliance with the License.
# You may obtain a copy of the License at
#
# http://www.apache.org/licenses/LICENSE-2.0
#
# Unless required by applicable law or agreed to in writing, software
# distributed under the License is distributed on an 'AS IS' BASIS,
# WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
# See the License for the specific language governing permissions and
# limitations under the License.

import argparse
from zipfile import ZipFile, ZipInfo

class ApkFile(ZipFile):
    def extract(self, member, path=None, pwd=None):
        if not isinstance(member, ZipInfo):
            member = self.getinfo(member)
        member.flag_bits ^= member.flag_bits%2
        ZipFile.extract(self, member, path, pwd)
        print 'extracting %s' % member.filename
                

    def extractall(self, path=None, members=None, pwd=None):
        map(lambda entry: self.extract(entry, path, pwd), members if members is not None  and len(members)>0 else self.filelist)
                
if __name__ == '__main__':
    parser = argparse.ArgumentParser(description='unpacks an APK that contains files which are wrongly marked as encrypted')
    parser.add_argument('apk', type=str)
    parser.add_argument('file', type=str, nargs='*')
    args = parser.parse_args()

    apk = ApkFile(args.apk,'r')
    apk.extractall(members=args.file)